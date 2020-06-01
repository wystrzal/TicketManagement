using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Infrastructure.Services.SearchIssueStrategy.ConcreteSearch;

namespace TicketManagement.API.Infrastructure.Services.SearchIssueStrategy
{
    public class SearchIssuesBox : ISearchIssuesBox
    {
        private readonly IIssueRepository issueRepository;

        public SearchIssuesBox(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        public ISearchIssues SearchIssues<T>() where T : class
        {
            var concreteSearches = new Hashtable();

            var type = typeof(T);

            if (!concreteSearches.ContainsKey(type))
            {
                var concreteSearch = Activator.CreateInstance(typeof(T), issueRepository);

                concreteSearches.Add(type, concreteSearch);
            }

            return concreteSearches[type] as ISearchIssues;          
        }
    }
}
