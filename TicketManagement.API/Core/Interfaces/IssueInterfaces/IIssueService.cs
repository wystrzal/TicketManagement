using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Core.Interfaces
{
    public interface IIssueService
    {
        Task<bool> AddNewIssue(NewIssueDto newIssue);
        Task<bool> ChangeIssueStatus(int issueId, Status status);
        Task<PaginatedItemsDto<GetIssueListDto>> GetIssues(SearchSpecificationDto searchSpecification);
        Task<GetIssueDto> GetIssue(int id);
        Task<List<GetIssueDepartamentsDto>> GetIssueDepartaments();


    }
}
