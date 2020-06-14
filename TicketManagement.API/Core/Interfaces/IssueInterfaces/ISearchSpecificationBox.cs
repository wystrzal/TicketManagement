using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces.IssueInterfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;

namespace TicketManagement.API.Core.Interfaces
{
    public interface ISearchSpecificationBox
    {
        void ConcreteSearch<T>(T typeOfSearch, SearchSpecificationDto searchSpecification) where T : class;
        Task<FilteredIssueListDto> Search(Expression<Func<Issue, bool>> searchFor, SearchSpecificationDto searchSpecification);
    }
}
