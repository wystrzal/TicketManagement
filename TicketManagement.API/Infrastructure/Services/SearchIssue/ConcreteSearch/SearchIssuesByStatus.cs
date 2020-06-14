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

namespace TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch
{
    public class SearchIssuesByStatus : IConcreteSearch
    {
        private readonly SearchSpecificationDto searchSpecification;

        public SearchIssuesByStatus(SearchSpecificationDto searchSpecification) 
        {
            this.searchSpecification = searchSpecification;
        }

        public Predicate<Issue> getSpecification()
        {
            return x => x.Status == searchSpecification.Status;
        }

    }
}
