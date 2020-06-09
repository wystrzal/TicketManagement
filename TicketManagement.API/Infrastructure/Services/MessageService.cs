using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Interfaces.MessageInterfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.MessageDtos;

namespace TicketManagement.API.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<GetIssueMessageDto> GetIssueMessage(int messageId)
        {
            var message = await unitOfWork.Repository<Message>().GetById(messageId);

            return mapper.Map<GetIssueMessageDto>(message);
        }

        public async Task<GetIssueMessageDto> AddNewMessage(NewMessageDto newMessage)
        {
            var messageToAdd = mapper.Map<Message>(newMessage);

            unitOfWork.Repository<Message>().Add(messageToAdd);

            if (await unitOfWork.SaveAllAsync())
            {
                var messageToReturn = await unitOfWork.Repository<Message>()
                    .GetByConditionWithIncludeFirst(x => x.Id == messageToAdd.Id, y => y.Sender);

                 return mapper.Map<GetIssueMessageDto>(messageToReturn);               
            }

            return null;       
        }

        public async Task<List<GetIssueMessageDto>> GetIssueMessages(int issueId)
        {
            var message = await unitOfWork.Repository<Message>()
                .GetByConditionWithIncludeToList(x => x.IssueId == issueId, y => y.Sender);

            return mapper.Map<List<GetIssueMessageDto>>(message);
        }
    }
}
