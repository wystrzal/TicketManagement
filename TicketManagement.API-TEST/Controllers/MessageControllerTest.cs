using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.Controllers;
using TicketManagement.API.Core.Interfaces.MessageInterfaces;
using TicketManagement.API.Dtos.MessageDtos;
using Xunit;

namespace TicketManagement.API_TEST.Controllers
{
    public class MessageControllerTest
    {
        private readonly Mock<IMessageService> messageService;
        public MessageControllerTest()
        {
            messageService = new Mock<IMessageService>();
        }

        [Fact]
        public async Task AddNewMessageModelStateIsNotValid()
        {
            //Arrange
            var newMessage = new NewMessageDto();

            var controller = new MessageController(messageService.Object);

            //Act
            var action = await controller.AddNewMessage(newMessage) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task AddNewMessageModelFailed()
        {
            //Arrange
            var newMessage = new NewMessageDto { Content = "test", IssueId = 1, SenderId = "test" };

            var controller = new MessageController(messageService.Object);

            messageService.Setup(x => x.AddNewMessage(newMessage)).Returns(Task.FromResult(false));

            //Act
            var action = await controller.AddNewMessage(newMessage) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task AddNewMessageModelSuccess()
        {
            //Arrange
            var newMessage = new NewMessageDto { Content = "test", IssueId = 1, SenderId = "test" };

            var controller = new MessageController(messageService.Object);

            messageService.Setup(x => x.AddNewMessage(newMessage)).Returns(Task.FromResult(true));

            //Act
            var action = await controller.AddNewMessage(newMessage) as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task GetIssueMessagesSuccess()
        {
            //Arrange
            var issueMessages = new List<GetIssueMessagesDto>()
            {
                new GetIssueMessagesDto {Content = "test"},
                new GetIssueMessagesDto {Content = "test"}
            };

            messageService.Setup(x => x.GetIssueMessages(It.IsAny<int>())).Returns(Task.FromResult(issueMessages));

            var controller = new MessageController(messageService.Object);

            //Act
            var action = await controller.GetIssueMessages(1) as OkObjectResult;
            var item = action.Value as List<GetIssueMessagesDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, item.Count);
        }
    }
}
