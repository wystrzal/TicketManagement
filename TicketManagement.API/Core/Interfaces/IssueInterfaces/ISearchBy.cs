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
        /// <summary>
        /// Search for specified issues.
        /// </summary>
        /// <param name="searchFor">Type what you want search e.g (x => x.DeclarantId == searchSpecification.UserId).</param>
        /// <param name="specification">Search issues by specified specification e.g (x => x.Status == Status.Close).</param>
        /// <param name="searchSpecification">Model with all specifications for search.</param>
        /// <returns></returns>        
        Task<FilteredIssueListDto> SearchIssues(Expression<Func<Issue, bool>> searchFor,
            Expression<Func<Issue, bool>> specification, SearchSpecificationDto searchSpecification);
    }
}
