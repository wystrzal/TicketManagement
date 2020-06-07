using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Dtos.MessageDtos;

namespace TicketManagement.API.Core.Interfaces.MessageInterfaces
{
    public interface IMessageService
    {
        Task<bool> AddNewMessage(NewMessageDto newMessage);
        Task<List<GetIssueMessagesDto>> GetIssueMessages(int issueId);
    }
}
