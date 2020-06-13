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
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch
{
    public abstract class SearchBy : ISearchBy
    {
        private readonly IIssueRepository issueRepository;
        private readonly Expression<Func<Issue, bool>> specification;
        private readonly SearchSpecificationDto searchSpecification;

        public SearchBy(IIssueRepository issueRepository, Expression<Func<Issue, bool>> specification,
            SearchSpecificationDto searchSpecification)
        {
            this.issueRepository = issueRepository;
            this.specification = specification;
            this.searchSpecification = searchSpecification;
        }

      
        public virtual async Task<FilteredIssueListDto> SearchIssues(Expression<Func<Issue, bool>> searchFor)
        {
            FilteredIssueListDto filteredIssueList = new FilteredIssueListDto();
            IssueCount issueCount = new IssueCount();

            //Compile expression from given parameters.
            var specificationValue = specification.Compile();
            var searchForValue = searchFor.Compile();

            if (searchSpecification.Title == null && searchSpecification.DeclarantLastName == null)
            {
                filteredIssueList.Issues = await issueRepository.GetIssues(x => specificationValue(x) && searchForValue(x),
                    searchSpecification.PageIndex, searchSpecification.PageSize);

                issueCount.FilteredIssue = await issueRepository.CountIssues(x => specificationValue(x) && searchForValue(x));
            }
            else
            {
                filteredIssueList = await SearchByContent(x => specificationValue(x) && searchForValue(x));
            }

            //Count issues by specific status 
            issueCount.NewIssue = await issueRepository.CountIssues(x => x.Status == Status.New && searchForValue(x));
            issueCount.OpenIssue = await issueRepository.CountIssues(x => x.Status == Status.Open && searchForValue(x));
            issueCount.ProgressIssue = await issueRepository.CountIssues(x => x.Status == Status.Progress && searchForValue(x));
            issueCount.PendingIssue = await issueRepository.CountIssues(x => x.Status == Status.Pending && searchForValue(x));

            filteredIssueList.Count = issueCount;

            return filteredIssueList;
        }

        //Search by content e.g title + specification from parameter.
        public async Task<FilteredIssueListDto> SearchByContent(Expression<Func<Issue, bool>> specification)
        {
            //Compile expression from given specification.
            var specificationValue = specification.Compile();
            searchSpecification.Title = searchSpecification.Title.ToLower();

            //searchSpecification.Title != null
            Func<Issue, bool> combindedSpecification = 
                x => x.Title.Contains(searchSpecification.Title) && specificationValue(x);

            if (searchSpecification.Title != null && searchSpecification.DeclarantLastName != null)
            {
                combindedSpecification = x => x.Declarant.Lastname.Contains(searchSpecification.DeclarantLastName)
                    && x.Title.Contains(searchSpecification.Title) && specificationValue(x);
            }
            else if (searchSpecification.DeclarantLastName != null)
            {
                combindedSpecification = x => x.Declarant.Lastname.Contains(searchSpecification.DeclarantLastName)
                    && specificationValue(x);
            } 

            var issues = await issueRepository
                .GetIssues(combindedSpecification, searchSpecification.PageIndex, searchSpecification.PageSize);

            var issueCount = new IssueCount
            {
                FilteredIssue = await issueRepository.CountIssues(combindedSpecification)
            };

            return new FilteredIssueListDto() { Issues = issues, Count = issueCount};
        }
    }
}
