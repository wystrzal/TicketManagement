using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;

namespace TicketManagement.API.Infrastructure.Services.SearchIssueStrategy.ConcreteSearch
{
    public class SearchIssuesByStatus : ISearchIssues
    {
        private readonly IIssueRepository issueRepository;

        public SearchIssuesByStatus(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        public async Task<PaginatedItemsDto<Issue>> SearchIssues(SearchSpecificationDto searchSpecification)
        {
            List<Issue> issues = await issueRepository.GetIssues(x => x.Status == searchSpecification.Status,
                searchSpecification.PageIndex, searchSpecification.PageSize);

            int totalIssues = await issueRepository.CountIssues(x => x.Status == searchSpecification.Status);

            PaginatedItemsDto<Issue> paginatedItems = new PaginatedItemsDto<Issue>(searchSpecification.PageIndex,
                totalIssues, issues, searchSpecification.PageSize);

            return paginatedItems;
        }
    }
}
