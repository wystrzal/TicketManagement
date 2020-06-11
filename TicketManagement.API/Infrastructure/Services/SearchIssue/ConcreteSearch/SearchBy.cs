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

        //Search Issues by specification.
        public virtual async Task<FilteredIssueListDto> SearchIssues(Expression<Func<Issue, bool>> searchFor)
        {
            FilteredIssueListDto filteredIssueList = new FilteredIssueListDto();

            //Compile expression from given parameters.
            var specificationValue = specification.Compile();
            var searchForValue = searchFor.Compile();

            if (searchSpecification.Title == null && searchSpecification.DeclarantLastName == null)
            {
                filteredIssueList.Issues = await issueRepository.GetIssues(x => specificationValue(x) && searchForValue(x),
                    searchSpecification.PageIndex, searchSpecification.PageSize);

                filteredIssueList.totalIssues = await issueRepository.CountIssues(x => specificationValue(x) && searchForValue(x));
            }
            else
            {
                filteredIssueList = await SearchByContent(x => specificationValue(x) && searchForValue(x));
            }

            return filteredIssueList;
        }

        //Search Issues by specification + content e.g. status + title.
        public async Task<FilteredIssueListDto> SearchByContent(Expression<Func<Issue, bool>> specification)
        {
            //Compile expression from given specification.
            var specificationValue = specification.Compile();

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
       
            var totalIssues = await issueRepository.CountIssues(combindedSpecification);

            return new FilteredIssueListDto() { Issues = issues, totalIssues = totalIssues };
        }
    }
}
