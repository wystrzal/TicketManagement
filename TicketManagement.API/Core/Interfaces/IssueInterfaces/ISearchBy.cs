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

        /// Summary:
        /// <summary>
        /// Search issues by specified specification.
        /// </summary>
        /// Parameters: 
        /// searchFor: Type what you want search
        /// e.g x => x.DeclarantId == searchSpecification.UserId;
        Task<FilteredIssueListDto> SearchIssues(Expression<Func<Issue, bool>> searchFor);
    }
}
