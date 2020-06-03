using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;

namespace TicketManagement.API.Dtos.IssueDtos
{
    public class FilteredIssueListDto
    {
        public List<Issue> Issues { get; set; }
        public int totalIssues { get; set; }
    }
}
