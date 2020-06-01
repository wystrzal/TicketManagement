using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.AccountDtos;
using TicketManagement.API.Dtos.IssueDtos;

namespace TicketManagement.API.Dtos
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //AccountDtos

            CreateMap<RegisterDto, User>();


            //IssueDtos

            CreateMap<NewIssueDto, Issue>();

        }
    }
}
