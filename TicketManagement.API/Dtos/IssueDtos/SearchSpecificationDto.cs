using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Dtos.IssueDtos
{
    public class SearchSpecificationDto
    {
        public string Departament { get; set; }
        public Status? Status { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public string DeclarantLastName { get; set; }
    }
}
