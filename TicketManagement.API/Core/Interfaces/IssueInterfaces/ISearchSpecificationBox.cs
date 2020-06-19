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
        /// <summary>
        /// Select type of search from concrete search class, and add it to list.
        /// </summary>
        /// <param name="typeOfSearch">Concrete search class.</param>
        /// <param name="searchSpecification">Specification typed by client to search issues.</param>
        void ConcreteSearch<T>(T typeOfSearch, SearchSpecificationDto searchSpecification) where T : class;

        /// <summary>
        /// Search issues by type of search list, added from ConcreteSearch method.
        /// </summary>
        /// <param name="searchFor">Type for what want to search e.g. user issues. </param>
        /// <param name="searchSpecification">Specification typed by client to search issues.</param>
        Task<FilteredIssueListDto> Search(Expression<Func<Issue, bool>> searchFor, SearchSpecificationDto searchSpecification);
    }
}
