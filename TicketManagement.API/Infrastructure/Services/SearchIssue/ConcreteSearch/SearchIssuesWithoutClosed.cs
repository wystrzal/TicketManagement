using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch
{
    public class SearchIssuesWithoutClosed : SearchByAbstract
    {
        public SearchIssuesWithoutClosed(IIssueRepository issueRepository, IMapper mapper) : base(issueRepository, mapper)
        {
        }

        public override async Task<PaginatedItemsDto<GetIssueListDto>> SearchIssues(SearchSpecificationDto searchSpecification)
        {
            PaginatedItemsDto<GetIssueListDto> paginatedItems = null;

            if (searchSpecification.Title == null && searchSpecification.DeclarantLastName == null)
            {
                List<Issue> issues = await issueRepository.GetIssues(x => x.Status != Status.Close,
                    searchSpecification.PageIndex, searchSpecification.PageSize);

                int totalIssues = await issueRepository.CountIssues(x => x.Status != Status.Close);

                var issuesToReturn = mapper.Map<List<GetIssueListDto>>(issues);

                paginatedItems = new PaginatedItemsDto<GetIssueListDto>(searchSpecification.PageIndex,
                    totalIssues, issuesToReturn, searchSpecification.PageSize);
            } 
            else
            {
                paginatedItems = await SearchByContent(searchSpecification, x => x.Status != Status.Close);
            }

            return paginatedItems;   
        }
    }
}
