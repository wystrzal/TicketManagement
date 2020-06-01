using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models
{
    public class SupportIssues
    {
        public string SupportId { get; set; }
        public User User { get; set; }
        public int IssueId { get; set; }
        public Issue Issue { get; set; }

    }
}
