using AutoMapper;
using System;
using System.Collections;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue
{
    public class SearchIssuesBox : ISearchIssuesBox
    {
        private readonly IIssueRepository issueRepository;

        public SearchIssuesBox(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        public SearchByAbstract SearchIssues<T>(SearchSpecificationDto searchSpecification) where T : class
        {
            var concreteSearches = new Hashtable();

            var type = typeof(T);

            if (!concreteSearches.ContainsKey(type))
            {
                var concreteSearch = Activator.CreateInstance(typeof(T), issueRepository, searchSpecification);

                concreteSearches.Add(type, concreteSearch);
            }

            return concreteSearches[type] as SearchByAbstract;          
        }
    }
}
