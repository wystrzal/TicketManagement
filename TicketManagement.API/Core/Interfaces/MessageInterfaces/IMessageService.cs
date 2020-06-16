using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.MessageDtos;

namespace TicketManagement.API.Core.Interfaces.MessageInterfaces
{
    public interface IMessageService
    {
        Task<GetIssueMessageDto> AddNewMessage(NewMessageDto newMessage);
        Task<List<GetIssueMessageDto>> GetIssueMessages(int issueId, bool supportMessages);
        Task<GetIssueMessageDto> GetIssueMessage(int messageId);
    }
}
