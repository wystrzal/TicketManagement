﻿using AutoMapper;
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
using static TicketManagement.API.Core.Models.Enums.IssuePriority;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;
using static TicketManagement.API.Core.Models.Enums.TypeOfSearch;

namespace TicketManagement.API.Infrastructure.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISearchSpecificationBox searchIssuesBox;
        private readonly IIssueRepository issueRepository;

        public IssueService(IUnitOfWork unitOfWork, ISearchSpecificationBox searchIssuesBox, IIssueRepository issueRepository)
        {
            this.unitOfWork = unitOfWork;
            this.searchIssuesBox = searchIssuesBox;
            this.issueRepository = issueRepository;
        }

        public async Task<bool> AddNewIssue(NewIssueDto newIssue)
        {
            var issueToAdd = unitOfWork.Mapper().Map<Issue>(newIssue);

            issueToAdd.Status = Status.New;
            issueToAdd.Priority = Priority.Medium;
            issueToAdd.Title = issueToAdd.Title.ToLower();

            unitOfWork.Repository<Issue>().Add(issueToAdd);

            return await unitOfWork.SaveAllAsync();
        }

        public async Task<bool> DeleteIssue(int issueId)
        {
            var issue = await unitOfWork.Repository<Issue>().GetById(issueId);

            unitOfWork.Repository<Issue>().Delete(issue);

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

        public async Task<bool> ChangeIssuePriority(int issueId, Priority priority)
        {
            var issue = await unitOfWork.Repository<Issue>().GetById(issueId);

            if (issue.Priority != priority)
            {
                issue.Priority = priority;
                return await unitOfWork.SaveAllAsync();
            }

            return true;
        }


        public async Task<GetIssueDto> GetIssue(int id)
        {
            var issue = await issueRepository.GetIssue(id);

            return unitOfWork.Mapper().Map<GetIssueDto>(issue);
        }

        public async Task<List<GetIssueSupportDto>> GetIssueSupport(int id)
        {
            var supportIssue = await unitOfWork.Repository<SupportIssues>()
                .GetByConditionWithIncludeToList(x => x.IssueId == id, y => y.User);

            return unitOfWork.Mapper().Map<List<GetIssueSupportDto>>(supportIssue);
        }

        public async Task<bool> AssignToIssue(int issueId, string supportId)
        {
            var supportIssue = new SupportIssues { IssueId = issueId, SupportId = supportId };

            unitOfWork.Repository<SupportIssues>().Add(supportIssue);

            return await unitOfWork.SaveAllAsync();
        }

        public async Task<bool> UnassignFromIssue(int issueId, string supportId)
        {
            var issue = await unitOfWork.Repository<SupportIssues>()
                .GetByConditionFirst(x => x.IssueId == issueId && x.SupportId == supportId);

            unitOfWork.Repository<SupportIssues>().Delete(issue);

            return await unitOfWork.SaveAllAsync();
        }

        public async Task<List<GetIssueDepartamentDto>> GetIssueDepartaments()
        {
            var departaments = await unitOfWork.Repository<Departament>().GetAll();

            return unitOfWork.Mapper().Map<List<GetIssueDepartamentDto>>(departaments);
        }

        public async Task<PaginatedItemsDto<GetIssueListDto, IssueCount>> GetIssues(SearchSpecificationDto searchSpecification)
        {        
            SetConcreteSearch(searchSpecification);

            var searchFor = SetSearchForSpecification(searchSpecification);

            var filteredIssueList = await searchIssuesBox.Search(searchFor, searchSpecification);

            var issuesToReturn = unitOfWork.Mapper().Map<List<GetIssueListDto>>(filteredIssueList.Issues);

            return new PaginatedItemsDto<GetIssueListDto, IssueCount>(searchSpecification.PageIndex, filteredIssueList.Count,
                issuesToReturn, searchSpecification.PageSize);
        }

        private void SetConcreteSearch(SearchSpecificationDto searchSpecification)
        {
            if (searchSpecification.Status != null)
            {
                searchIssuesBox.ConcreteSearch(typeof(SearchIssuesByStatus), searchSpecification);
            }
            else
            {
                searchIssuesBox.ConcreteSearch(typeof(SearchIssuesWithoutClosed), searchSpecification);
            }

            if (searchSpecification.Departament != null)
            {
                searchIssuesBox.ConcreteSearch(typeof(SearchIssuesByDepartament), searchSpecification);
            }
            if (searchSpecification.Title != null)
            {
                searchIssuesBox.ConcreteSearch(typeof(SearchIssuesByTitle), searchSpecification);
            }
            if (searchSpecification.DeclarantLastName != null)
            {
                searchIssuesBox.ConcreteSearch(typeof(SearchIssuesByDeclarantLastName), searchSpecification);
            }
            if (searchSpecification.Priority != null)
            {
                searchIssuesBox.ConcreteSearch(typeof(SearchIssuesByPriority), searchSpecification);
            }
        }

        private Expression<Func<Issue, bool>> SetSearchForSpecification(SearchSpecificationDto searchSpecification)
        {
            Expression<Func<Issue, bool>> searchFor = null;

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

            return searchFor;
        }
    }
}
