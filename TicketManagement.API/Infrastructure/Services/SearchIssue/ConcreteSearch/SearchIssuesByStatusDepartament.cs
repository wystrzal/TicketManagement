using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch
{
    public class SearchIssuesByStatusDepartament : SearchByAbstract
    {
        public SearchIssuesByStatusDepartament(IIssueRepository issueRepository, IMapper mapper) : base(issueRepository, mapper)
        {
        }

        public override async Task<PaginatedItemsDto<GetIssueListDto>> SearchIssues(SearchSpecificationDto searchSpecification)
        {
            PaginatedItemsDto<GetIssueListDto> paginatedItems = null;

            if (searchSpecification.Title == null && searchSpecification.DeclarantLastName == null)
            {
                List<Issue> issues = await issueRepository.GetIssues(x => x.Status == searchSpecification.Status && 
                x.Declarant.Departament.Name == searchSpecification.Departament, searchSpecification.PageIndex,
                searchSpecification.PageSize);

                var issuesToReturn = mapper.Map<List<GetIssueListDto>>(issues);

                int totalIssues = await issueRepository.CountIssues(x => x.Status == searchSpecification.Status &&
                x.Declarant.Departament.Name == searchSpecification.Departament);

                paginatedItems = new PaginatedItemsDto<GetIssueListDto>(searchSpecification.PageIndex,
                    totalIssues, issuesToReturn, searchSpecification.PageSize);
            }
            else
            {
                paginatedItems = await SearchByContent(searchSpecification, x => x.Status == searchSpecification.Status &&
                x.Declarant.Departament.Name == searchSpecification.Departament);
            }

            return paginatedItems;
        }
    }
}
