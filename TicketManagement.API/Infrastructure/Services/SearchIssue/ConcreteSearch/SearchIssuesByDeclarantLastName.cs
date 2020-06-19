using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces.IssueInterfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.IssueDtos;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch
{
    public class SearchIssuesByDeclarantLastName : IConcreteSearch
    {
        private readonly SearchSpecificationDto searchSpecification;

        public SearchIssuesByDeclarantLastName(SearchSpecificationDto searchSpecification)
        {
            this.searchSpecification = searchSpecification;
        }

        public Predicate<Issue> getTypeOfSearch()
        {
            return x => x.Declarant.Lastname.Contains(searchSpecification.DeclarantLastName);
        }
    }   
}
