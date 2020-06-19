using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Interfaces.IssueInterfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch
{
    public class SearchIssuesWithoutClosed : IConcreteSearch
    {
        private readonly SearchSpecificationDto searchSpecification;

        public SearchIssuesWithoutClosed(SearchSpecificationDto searchSpecification)
        {
            this.searchSpecification = searchSpecification;
        }

        public Predicate<Issue> getTypeOfSearch()
        {
            return x => x.Status != Status.Close;
        }
    }
}
