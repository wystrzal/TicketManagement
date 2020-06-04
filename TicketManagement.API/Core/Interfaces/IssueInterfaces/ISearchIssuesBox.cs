using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;

namespace TicketManagement.API.Core.Interfaces
{
    public interface ISearchIssuesBox
    {
        ISearchBy ConcreteSearch<T>(T typeOfSearch, SearchSpecificationDto searchSpecification) where T : class;
    }
}
