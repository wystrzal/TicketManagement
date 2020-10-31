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

namespace TicketManagement.API.Infrastructure.Services.SearchIssue
{
    public class SearchBy : ISearchBy
    {
        private readonly IIssueRepository issueRepository;

        public SearchBy(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }
  
        public virtual async Task<FilteredIssueListDto> SearchIssues(Expression<Func<Issue, bool>> searchFor,
            Expression<Func<Issue, bool>> specification, SearchSpecificationDto searchSpecification)
        {
            FilteredIssueListDto filteredIssueList = new FilteredIssueListDto();
            IssueCount issueCount = new IssueCount();

            var specificationValue = specification.Compile();
            var searchForValue = searchFor.Compile();

            filteredIssueList.Issues = await issueRepository.GetIssues(x => specificationValue(x) && searchForValue(x),
                searchSpecification.PageIndex, searchSpecification.PageSize);

            issueCount.FilteredIssue = await issueRepository.CountIssues(x => specificationValue(x) && searchForValue(x));

            await CountSpecificStatusIssues(issueCount, searchForValue);

            filteredIssueList.Count = issueCount;

            return filteredIssueList;
        }

        private async Task CountSpecificStatusIssues(IssueCount issueCount, Func<Issue, bool> searchForValue)
        {
            issueCount.NewIssue = await issueRepository.CountIssues(x => x.Status == Status.New && searchForValue(x));
            issueCount.OpenIssue = await issueRepository.CountIssues(x => x.Status == Status.Open && searchForValue(x));
            issueCount.ProgressIssue = await issueRepository.CountIssues(x => x.Status == Status.Progress && searchForValue(x));
            issueCount.PendingIssue = await issueRepository.CountIssues(x => x.Status == Status.Pending && searchForValue(x));
        }
    }
}
