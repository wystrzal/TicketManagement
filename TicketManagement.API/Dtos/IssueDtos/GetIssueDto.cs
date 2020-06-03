using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Dtos.IssueDtos
{
    public class GetIssueDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string DeclarantId { get; set; }
    }
}
