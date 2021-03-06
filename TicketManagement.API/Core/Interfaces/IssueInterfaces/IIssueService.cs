﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.IssueDtos;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using static TicketManagement.API.Core.Models.Enums.IssuePriority;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Core.Interfaces
{
    public interface IIssueService
    {
        Task<bool> AddNewIssue(NewIssueDto newIssue);
        Task<bool> DeleteIssue(int issueId);
        Task<bool> ChangeIssueStatus(int issueId, Status status);
        Task<bool> ChangeIssuePriority(int issueId, Priority priority);
        Task<PaginatedItemsDto<GetIssueListDto, IssueCount>> GetIssues(SearchSpecificationDto searchSpecification);
        Task<GetIssueDto> GetIssue(int id);
        Task<List<GetIssueDepartamentDto>> GetIssueDepartaments();

        /// <summary>
        /// Return assigned support to concrete issue.
        /// </summary>
        Task<List<GetIssueSupportDto>> GetIssueSupport(int id);
        Task<bool> AssignToIssue(int issueId, string supportId);
        Task<bool> UnassignFromIssue(int issueId, string supportId);

    }
}
