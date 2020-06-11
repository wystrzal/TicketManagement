using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.IssueDtos;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.AccountDtos;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Dtos.MessageDtos;

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

            CreateMap<Issue, GetIssueListDto>()
                .ForMember(x => x.Declarant, opt =>
                opt.MapFrom(src => src.Declarant.Firstname + " " + src.Declarant.Lastname))
                .ForMember(x => x.Departament, opt =>
                opt.MapFrom(src => src.Declarant.Departament.Name));

            CreateMap<Issue, GetIssueDto>()
                .ForMember(x => x.Declarant, opt =>
                opt.MapFrom(src => src.Declarant.Firstname + " " + src.Declarant.Lastname))
                .ForMember(x => x.Departament, opt =>
                opt.MapFrom(src => src.Declarant.Departament.Name));

            CreateMap<Departament, GetIssueDepartamentDto>();

            CreateMap<SupportIssues, GetIssueSupportDto>()
                .ForMember(x => x.SupportId, opt =>
                opt.MapFrom(src => src.User.Id))
                .ForMember(x => x.SupportName, opt =>
                opt.MapFrom(src => src.User.Firstname + " " + src.User.Lastname));

            //MessageDtos

            CreateMap<NewMessageDto, Message>();

            CreateMap<Message, GetIssueMessageDto>()
                .ForMember(x => x.Sender, opt =>
                opt.MapFrom(src => src.Sender.Firstname + " " + src.Sender.Lastname));
        }
    }
}
