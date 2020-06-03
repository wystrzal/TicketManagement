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
        private readonly IIssueRepository issueRepository;
        private readonly Expression<Func<Issue, bool>> specification;
        private readonly SearchSpecificationDto searchSpecification;

        public SearchByAbstract(IIssueRepository issueRepository, Expression<Func<Issue, bool>> specification,
            SearchSpecificationDto searchSpecification)
        {
            this.issueRepository = issueRepository;
            this.specification = specification;
            this.searchSpecification = searchSpecification;
        }

        //Search Issues by specification.
        public virtual async Task<FilteredIssueListDto> SearchIssues(Expression<Func<Issue, bool>> typeOfSearch)
        {
            FilteredIssueListDto filteredIssueList = new FilteredIssueListDto();

            var specificationValue = specification.Compile();
            var typeOfSearchValue = typeOfSearch.Compile();

            if (searchSpecification.Title == null && searchSpecification.DeclarantLastName == null)
            {
                filteredIssueList.Issues = await issueRepository.GetIssues(x => specificationValue(x) && typeOfSearchValue(x),
                    searchSpecification.PageIndex, searchSpecification.PageSize);

                filteredIssueList.totalIssues = await issueRepository.CountIssues(x => specificationValue(x) && typeOfSearchValue(x));
            }
            else
            {
                filteredIssueList = await SearchByContent(x => specification.Compile()(x) && typeOfSearch.Compile()(x));
            }


            return filteredIssueList;
        }

        //Search Issues by specification + content e.g. status + title.
        public virtual async Task<FilteredIssueListDto> SearchByContent(Expression<Func<Issue, bool>> specification)
        {
            FilteredIssueListDto filteredIssueList = new FilteredIssueListDto();
            //Compile expression from given specification.
            var specificationValue = specification.Compile();

            if (searchSpecification.Title != null)
            {
                filteredIssueList.Issues = await issueRepository.GetIssues(x => x.Title.Contains(searchSpecification.Title) 
                    && specificationValue(x), searchSpecification.PageIndex, searchSpecification.PageSize);

                filteredIssueList.totalIssues = await issueRepository.CountIssues(
                    x => x.Title.Contains(searchSpecification.Title)
                    && specificationValue(x));
            }
            else 
            {
                filteredIssueList.Issues = await issueRepository
                    .GetIssues(x => x.Declarant.Lastname.Contains(searchSpecification.DeclarantLastName)
                    && specificationValue(x), searchSpecification.PageIndex, searchSpecification.PageSize);

                filteredIssueList.totalIssues = await issueRepository.CountIssues(
                    x => x.Declarant.Lastname.Contains(searchSpecification.DeclarantLastName)
                    && specificationValue(x));
            }

            return filteredIssueList;
        }
    }
}
