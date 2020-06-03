﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;

namespace TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch
{
    public class SearchIssuesByStatus : SearchByAbstract
    {
        public SearchIssuesByStatus(IIssueRepository issueRepository, SearchSpecificationDto searchSpecification) 
            : base(issueRepository, x => x.Status == searchSpecification.Status, searchSpecification)
        {
        }
    }
}
