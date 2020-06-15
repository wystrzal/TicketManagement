using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models.Enums;
using static TicketManagement.API.Core.Models.Enums.IssuePriority;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Core.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public string DeclarantId { get; set; }
        public User Declarant { get; set; }

        //Employees who support this issue.
        public virtual ICollection<SupportIssues> SupportIssues { get; set; }

    }
}
