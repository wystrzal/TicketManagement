﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using static TicketManagement.API.Core.Models.Enums.IssuePriority;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;
using static TicketManagement.API.Core.Models.Enums.TypeOfSearch;

namespace TicketManagement.API.Dtos.IssueDtos
{
    public class SearchSpecificationDto
    {
        public string Departament { get; set; }
        public Status? Status { get; set; }
        public Priority? Priority { get; set; }
        public string Title { get; set; }
        public string DeclarantLastName { get; set; }
        public string UserId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public SearchFor SearchFor { get; set; }

        public SearchSpecificationDto()
        {
            PageIndex = 1;
            PageSize = 25;
            SearchFor = SearchFor.AllIssues;
        }
    }
}
