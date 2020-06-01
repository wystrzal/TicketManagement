using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;

namespace TicketManagement.API.Controllers
{
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
            if (await issueService.AddNewIssue(newIssue))
            {
                return Ok();
            }

            return BadRequest("Something goes wrong.");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetIssues([FromQuery]SearchSpecificationDto searchSpecification)
        {
            PaginatedItemsDto<Issue> paginatedItems = await issueService.GetIssues(searchSpecification);

            return Ok(paginatedItems);
        }


    }
}