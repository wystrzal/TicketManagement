using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;

namespace TicketManagement.API.Core.Interfaces
{
    public interface IIssueService
    {
        Task<bool> AddNewIssue(NewIssueDto newIssue);
        Task<PaginatedItemsDto<Issue>> GetIssues(SearchSpecificationDto searchSpecification);
    }
}
