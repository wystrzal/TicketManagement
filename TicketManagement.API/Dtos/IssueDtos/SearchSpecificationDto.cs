using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;
using static TicketManagement.API.Core.Models.Enums.TypeOfSearch;

namespace TicketManagement.API.Dtos.IssueDtos
{
    public class SearchSpecificationDto
    {
        public string Departament { get; set; }
        public Status? Status { get; set; }
        public string Title { get; set; }
        public string DeclarantLastName { get; set; }
        public string UserId { get; set; }

        [Required]
        public int PageIndex { get; set; }

        [Required]
        public int PageSize { get; set; }

        [Required]
        public SearchFor SearchFor { get; set; }
    }
}
