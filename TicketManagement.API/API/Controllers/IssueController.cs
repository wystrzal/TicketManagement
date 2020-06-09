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
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Controllers
{

    [AllowAnonymous]
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
            if (ModelState.IsValid)
            {
                if (await issueService.AddNewIssue(newIssue))
                {
                    return Ok();
                }

                return BadRequest("Something goes wrong.");
            }

            return BadRequest("Model state is not valid.");
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
    }
}