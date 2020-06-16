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

        public MessageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<GetIssueMessageDto> GetIssueMessage(int messageId)
        {
            var message = await unitOfWork.Repository<Message>().GetById(messageId);

            return unitOfWork.Mapper().Map<GetIssueMessageDto>(message);
        }

        public async Task<GetIssueMessageDto> AddNewMessage(NewMessageDto newMessage)
        {
            var messageToAdd = unitOfWork.Mapper().Map<Message>(newMessage);

            unitOfWork.Repository<Message>().Add(messageToAdd);

            if (await unitOfWork.SaveAllAsync())
            {
                var messageToReturn = await unitOfWork.Repository<Message>()
                    .GetByConditionWithIncludeFirst(x => x.Id == messageToAdd.Id, y => y.Sender);

                 return unitOfWork.Mapper().Map<GetIssueMessageDto>(messageToReturn);               
            }

            return null;       
        }

        //TEST
        public async Task<List<GetIssueMessageDto>> GetIssueMessages(int issueId, bool supportMessages)
        {
            List<Message> message = null;

            if (supportMessages)
            {
                message = await unitOfWork.Repository<Message>()
                    .GetByConditionWithIncludeToList(x => x.IssueId == issueId && x.IsSupportMessage == true, y => y.Sender);
            } 
            else
            {
                message = await unitOfWork.Repository<Message>()
                    .GetByConditionWithIncludeToList(x => x.IssueId == issueId && x.IsSupportMessage == false, y => y.Sender);
            }

            return unitOfWork.Mapper().Map<List<GetIssueMessageDto>>(message);
        }
    }
}
