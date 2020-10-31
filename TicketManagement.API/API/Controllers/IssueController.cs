using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using static TicketManagement.API.Core.Models.Enums.IssuePriority;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Controllers
{

    [Authorize(Policy = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService issueService;

        public IssueController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIssue(NewIssueDto newIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state is not valid.");
            }

            if (await issueService.AddNewIssue(newIssue))
            {
                return Ok();
            }

            return BadRequest("Something goes wrong.");
        }

        [HttpDelete("{issueId}")]
        public async Task<IActionResult> DeleteIssue(int issueId)
        {
            if (await issueService.DeleteIssue(issueId))
            {
                return Ok();
            }

            return BadRequest("Something goes wrong.");
        }

        [HttpPost("{id}/status/{status}")]
        public async Task<IActionResult> ChangeIssueStatus(int id, Status status)
        {
            if (await issueService.ChangeIssueStatus(id, status))
            {
                return Ok();
            }

            return BadRequest("Something goes wrong.");
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("{id}/priority/{priority}")]
        public async Task<IActionResult> ChangeIssuePriority(int id, Priority priority)
        {
            if (await issueService.ChangeIssuePriority(id, priority))
            {
                return Ok();
            }

            return BadRequest("Something goes wrong.");
        }

        [HttpGet]
        public async Task<IActionResult> GetIssues([FromQuery]SearchSpecificationDto searchSpecification)
        {
            return Ok(await issueService.GetIssues(searchSpecification));
        }

        [HttpGet("departament")]
        public async Task<IActionResult> GetIssueDepartaments()
        {
            return Ok(await issueService.GetIssueDepartaments());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssue(int id)
        {
            GetIssueDto issue = await issueService.GetIssue(id);

            if (issue != null)
            {
                return Ok(issue);
            }

            return BadRequest("Something goes wrong.");
        }

        [HttpGet("{id}/support")]
        public async Task<IActionResult> GetIssueSupport(int id)
        {
            return Ok(await issueService.GetIssueSupport(id));
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("{issueId}/assign/{supportId}")]
        public async Task<IActionResult> AssignToIssue(int issueId, string supportId)
        {
            if (await issueService.AssignToIssue(issueId, supportId))
            {
                return Ok();
            }

            return BadRequest("Something goes wrong");
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("{issueId}/unassign/{supportId}")]
        public async Task<IActionResult> UnassignFromIssue(int issueId, string supportId)
        {
            if (await issueService.UnassignFromIssue(issueId, supportId))
            {
                return Ok();
            }

            return BadRequest("Something goes wrong");
        }
    }
}