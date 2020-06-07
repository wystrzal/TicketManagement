using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.API.Core.Interfaces.MessageInterfaces;
using TicketManagement.API.Dtos.MessageDtos;

namespace TicketManagement.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMessage(NewMessageDto newMessage)
        {
            if (ModelState.IsValid)
            {
                if (await messageService.AddNewMessage(newMessage))
                {
                    return Ok();
                }

                return BadRequest("Something goes wrong");
            }

            return BadRequest("Model state is valid");
        }

        [HttpGet("{issueId}")]
        public async Task<IActionResult> GetIssueMessages(int issueId)
        {
            return Ok(await messageService.GetIssueMessages(issueId));
        }
    }
}