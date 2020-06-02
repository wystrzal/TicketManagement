using AutoMapper;
using System;
using System.Collections;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue
{
    public class SearchIssuesBox : ISearchIssuesBox
    {
        private readonly IIssueRepository issueRepository;
        private readonly IMapper mapper;

        public SearchIssuesBox(IIssueRepository issueRepository, IMapper mapper)
        {
            this.issueRepository = issueRepository;
            this.mapper = mapper;
        }

        public SearchByAbstract SearchIssues<T>() where T : class
        {
            var concreteSearches = new Hashtable();

            var type = typeof(T);

            if (!concreteSearches.ContainsKey(type))
            {
                var concreteSearch = Activator.CreateInstance(typeof(T), issueRepository, mapper);

                concreteSearches.Add(type, concreteSearch);
            }

            return concreteSearches[type] as SearchByAbstract;          
        }
    }
}
