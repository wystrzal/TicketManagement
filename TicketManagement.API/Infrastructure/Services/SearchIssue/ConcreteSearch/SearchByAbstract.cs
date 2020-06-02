using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
namespace TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch
{
    public abstract class SearchByAbstract
    {
        protected readonly IIssueRepository issueRepository;
        protected readonly IMapper mapper;

        public SearchByAbstract(IIssueRepository issueRepository, IMapper mapper)
        {
            this.issueRepository = issueRepository;
            this.mapper = mapper;
        }

        //Search Issues by specification.
        public abstract Task<PaginatedItemsDto<GetIssueListDto>> SearchIssues(SearchSpecificationDto searchSpecification);

        //Search Issues by specification + content e.g. status + title.
        protected async Task<PaginatedItemsDto<GetIssueListDto>> SearchByContent(SearchSpecificationDto searchSpecification, 
            Expression<Func<Issue, bool>> specification)
        {
            List<Issue> issues = null;
            int totalIssues = 0;

            if (searchSpecification.Title != null)
            {

                var exprVal = specification.Compile();

                issues = await issueRepository.GetIssuesWithContent(x => x.Title.Contains(searchSpecification.Title) || exprVal(x),
                    null, searchSpecification.PageIndex, searchSpecification.PageSize);

                totalIssues = await issueRepository.CountIssuesWithContent(x => x.Title.Contains(searchSpecification.Title),
                    specification);
            }
            else 
            {
                issues = await issueRepository
                    .GetIssuesWithContent(x => x.Declarant.Lastname.Contains(searchSpecification.DeclarantLastName),
                    specification, searchSpecification.PageIndex, searchSpecification.PageSize);

                totalIssues = await issueRepository.CountIssuesWithContent(
                    x => x.Declarant.Lastname.Contains(searchSpecification.DeclarantLastName),
                    specification);
            }

            var issuesToReturn = mapper.Map<List<GetIssueListDto>>(issues);

            return new PaginatedItemsDto<GetIssueListDto>(searchSpecification.PageIndex,
                totalIssues, issuesToReturn, searchSpecification.PageSize);

        }
    }
}
