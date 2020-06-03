using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

            unitOfWork.Repository<Issue>().Add(issueToAdd);

            if (await unitOfWork.SaveAllAsync())
            {
                return true;
            }

            return false;
        }

        public async Task<bool> ChangeIssueStatus(int issueId, Status status)
        {
            var issue = await unitOfWork.Repository<Issue>().GetById(issueId);

            issue.Status = status;

            if (await unitOfWork.SaveAllAsync())
            {
                return true;
            }

            return false;
        }


        public async Task<GetIssueDto> GetIssue(int id)
        {
            var issue = await unitOfWork.Repository<Issue>().GetById(id);

            if (issue == null)
            {
                return null;
            }

            return mapper.Map<GetIssueDto>(issue);
        }

        public async Task<PaginatedItemsDto<GetIssueListDto>> GetIssues(SearchSpecificationDto searchSpecification)
        {
            Expression<Func<Issue, bool>> typeOfSearch = null;
            FilteredIssueListDto filteredIssueList = null;

            //Choose type of search.
            switch (searchSpecification.SearchFor)
            {
                case SearchFor.UserIssues:
                    typeOfSearch = x => x.DeclarantId == searchSpecification.UserId;
                    break;
                case SearchFor.SupportIssues:
                    typeOfSearch = x => x.SupportIssues.Where(x => x.SupportId == searchSpecification.UserId).Any();
                    break;
                case SearchFor.AllIssues:
                    typeOfSearch = x => x.Id != 0;
                    break;
                default:
                    break;
            }

            //Search by specification
            if (searchSpecification.Status != null && searchSpecification.Departament != null)
            {
                filteredIssueList = await searchIssuesBox.SearchIssues<SearchIssuesByStatusDepartament>(searchSpecification)
                    .SearchIssues(typeOfSearch);
            }
            else if (searchSpecification.Status != null)
            {
                filteredIssueList = await searchIssuesBox.SearchIssues<SearchIssuesByStatus>(searchSpecification)
                    .SearchIssues(typeOfSearch);
            } 
            else if (searchSpecification.Departament != null)
            {
                filteredIssueList = await searchIssuesBox.SearchIssues<SearchIssuesByDepartament>(searchSpecification)
                    .SearchIssues(typeOfSearch);
            }
            else
            {
                filteredIssueList = await searchIssuesBox.SearchIssues<SearchIssuesWithoutClosed>(searchSpecification)
                    .SearchIssues(typeOfSearch);
            }

            var issuesToReturn = mapper.Map<List<GetIssueListDto>>(filteredIssueList.Issues);

            return new PaginatedItemsDto<GetIssueListDto>(searchSpecification.PageIndex, filteredIssueList.totalIssues,
                issuesToReturn, searchSpecification.PageSize);
        }

    }
}
