using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.IssueDtos;

namespace TicketManagement.API.Core.Interfaces
{
    public interface ISearchBy
    {
        /// Parameters: 
        /// searchFor: Type what you want search
        /// e.g (x => x.DeclarantId == searchSpecification.UserId)
        /// specification: Search issues by specified specification
        /// e.g (x => x.Status == Status.Close)
        /// searchSpecification: Model with all specifications for search
        Task<FilteredIssueListDto> SearchIssues(Expression<Func<Issue, bool>> searchFor,
            Expression<Func<Issue, bool>> specification, SearchSpecificationDto searchSpecification);
    }
}
