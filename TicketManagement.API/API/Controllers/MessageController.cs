﻿using System;
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
    [Authorize(Policy = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetIssueMessage(int messageId)
        {
            var message = await messageService.GetIssueMessage(messageId);

            if (message == null)
            {
                return BadRequest("Something goes wrong");
            }

            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMessage(NewMessageDto newMessage)
        {
            if (ModelState.IsValid)
            {
                GetIssueMessageDto message = await messageService.AddNewMessage(newMessage);

                if (message != null)
                {
                    return CreatedAtAction("GetIssueMessage", new { messageId = message.Id }, message);
                }

                return BadRequest("Something goes wrong");
            }

            return BadRequest("Model state is valid");
        }

        [HttpGet("issue/{issueId}/support/{supportMessages}")]
        public async Task<IActionResult> GetIssueMessages(int issueId, bool supportMessages)
        {
            return Ok(await messageService.GetIssueMessages(issueId, supportMessages));
        }
    }
}