using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models
{
    public class IssueCount
    {
        public int FilteredIssue { get; set; }
        public int NewIssue { get; set; }
        public int OpenIssue { get; set; }
        public int ProgressIssue { get; set; }
        public int PendingIssue { get; set; }
    }
}
