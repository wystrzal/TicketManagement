using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Dtos.MessageDtos
{
    public class NewMessageDto
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int IssueId { get; set; }

        [Required]
        public string SenderId { get; set; }
    }
}
