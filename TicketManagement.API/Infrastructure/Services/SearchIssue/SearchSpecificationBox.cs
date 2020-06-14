using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Interfaces.IssueInterfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue
{
    public class SearchSpecificationBox : ISearchSpecificationBox
    {
        private readonly ISearchBy searchBy;
        private readonly List<Predicate<Issue>> predicates;

        public SearchSpecificationBox(ISearchBy searchBy)
        {
            this.searchBy = searchBy;
            predicates = new List<Predicate<Issue>>();
        }

        public void ConcreteSearch<T>(T typeOfSearch, SearchSpecificationDto searchSpecification) where T : class
        {
            var concreteSearches = new Hashtable();

            var type = typeOfSearch;

            if (!concreteSearches.ContainsKey(type))
            {
                var concreteSearch = Activator.CreateInstance(type as Type, searchSpecification);

                concreteSearches.Add(type, concreteSearch);
            }

            var selectedSearch = concreteSearches[type] as IConcreteSearch;

            predicates.Add(selectedSearch.getSpecification());
        }

        public async Task<FilteredIssueListDto> Search(Expression<Func<Issue, bool>> searchFor, SearchSpecificationDto searchSpecification)
        {
            Expression<Func<Issue, bool>> expression = x => predicates.All(all => all(x));

            var issues = await searchBy.SearchIssues(searchFor, expression, searchSpecification);

            predicates.Clear();

            return issues;
        }
    }
}
