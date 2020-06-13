using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.IssueDtos;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;
using static TicketManagement.API.Core.Models.Enums.TypeOfSearch;

namespace TicketManagement.API.Infrastructure.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISearchIssuesBox searchIssuesBox;

        public IssueService(IUnitOfWork unitOfWork, IMapper mapper, ISearchIssuesBox searchIssuesBox)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.searchIssuesBox = searchIssuesBox;
        }

        public async Task<bool> AddNewIssue(NewIssueDto newIssue)
        {
            var issueToAdd = mapper.Map<Issue>(newIssue);

            issueToAdd.Status = Status.New;
            issueToAdd.Title = issueToAdd.Title.ToLower();

            unitOfWork.Repository<Issue>().Add(issueToAdd);

            return await unitOfWork.SaveAllAsync();
        }

        public async Task<bool> ChangeIssueStatus(int issueId, Status status)
        {
            var issue = await unitOfWork.Repository<Issue>().GetById(issueId);

            if (issue.Status != status)
            {
                issue.Status = status;
                return await unitOfWork.SaveAllAsync();
            }

            return true;
        }


        public async Task<GetIssueDto> GetIssue(int id)
        {
            var issue = await unitOfWork.Repository<Issue>()
                .GetByConditionWithIncludeFirst(x => x.Id == id, y => y.Declarant.Departament);

            return mapper.Map<GetIssueDto>(issue);
        }

        public async Task<List<GetIssueSupportDto>> GetIssueSupport(int id)
        {
            var supportIssue = await unitOfWork.Repository<SupportIssues>()
                .GetByConditionWithIncludeToList(x => x.IssueId == id, y => y.User);

            return mapper.Map<List<GetIssueSupportDto>>(supportIssue);
        }

        public async Task<bool> AssignToIssue(int issueId, string supportId)
        {
            var supportIssue = new SupportIssues { IssueId = issueId, SupportId = supportId };

            unitOfWork.Repository<SupportIssues>().Add(supportIssue);

            if (await unitOfWork.SaveAllAsync())
            {
                return true;
            }

            return false;
        }

        public async Task<List<GetIssueDepartamentDto>> GetIssueDepartaments()
        {
            var departaments = await unitOfWork.Repository<Departament>().GetAll();

            return mapper.Map<List<GetIssueDepartamentDto>>(departaments);
        }

        public async Task<PaginatedItemsDto<GetIssueListDto, IssueCount>> GetIssues(SearchSpecificationDto searchSpecification)
        {
            Expression<Func<Issue, bool>> searchFor = null;
            FilteredIssueListDto filteredIssueList = null;

            var typeOfSearch = typeof(SearchIssuesWithoutClosed);

            //Choose for what must search.
            switch (searchSpecification.SearchFor)
            {
                case SearchFor.UserIssues:
                    searchFor = x => x.DeclarantId == searchSpecification.UserId;
                    break;
                case SearchFor.SupportIssues:
                    searchFor = x => x.SupportIssues.Where(x => x.SupportId == searchSpecification.UserId).Any();
                    break;
                case SearchFor.AllIssues:
                    searchFor = x => x.Id != 0;
                    break;
                default:
                    break;
            }

            //Choose type of search.
            if (searchSpecification.Status != null && searchSpecification.Departament != null)
            {
                typeOfSearch = typeof(SearchIssuesByStatusDepartament);
            }
            else if (searchSpecification.Status != null)
            {
                typeOfSearch = typeof(SearchIssuesByStatus);
            } 
            else if (searchSpecification.Departament != null)
            {
                typeOfSearch = typeof(SearchIssuesByDepartament);
            }

            filteredIssueList = await searchIssuesBox.ConcreteSearch(typeOfSearch, searchSpecification)
                .SearchIssues(searchFor);

            var issuesToReturn = mapper.Map<List<GetIssueListDto>>(filteredIssueList.Issues);

            return new PaginatedItemsDto<GetIssueListDto, IssueCount>(searchSpecification.PageIndex, filteredIssueList.Count,
                issuesToReturn, searchSpecification.PageSize);
        }

    }
}
