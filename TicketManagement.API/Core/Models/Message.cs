using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }
        public int SenderId { get; set; }
    }
}