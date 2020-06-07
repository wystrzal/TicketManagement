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

        public async Task<bool> AddNewMessage(NewMessageDto newMessage)
        {
            var messageToAdd = mapper.Map<Message>(newMessage);

            unitOfWork.Repository<Message>().Add(messageToAdd);

            return await unitOfWork.SaveAllAsync();
        }

        public async Task<List<GetIssueMessagesDto>> GetIssueMessages(int issueId)
        {
            var message = await unitOfWork.Repository<Message>()
                .GetByConditionWithIncludeToList(x => x.IssueId == issueId, y => y.Sender);

            return mapper.Map<List<GetIssueMessagesDto>>(message);
        }
    }
}
