﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Dtos.MessageDtos
{
    public class GetIssueMessagesDto
    {
        public string Content { get; set; }
        public string SenderId { get; set; }
        public string Sender { get; set; }
    }
}
