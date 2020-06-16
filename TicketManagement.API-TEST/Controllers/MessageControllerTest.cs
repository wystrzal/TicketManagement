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
        public async Task AddNewMessageFailed()
        {
            //Arrange
            var newMessage = new NewMessageDto { Content = "test", IssueId = 1, SenderId = "test" };

            var controller = new MessageController(messageService.Object);

            messageService.Setup(x => x.AddNewMessage(newMessage)).Returns(Task.FromResult((GetIssueMessageDto)null));

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
            var issueMessage = new GetIssueMessageDto { Content = "test" };

            var controller = new MessageController(messageService.Object);

            messageService.Setup(x => x.AddNewMessage(newMessage)).Returns(Task.FromResult(issueMessage));

            //Act
            var action = await controller.AddNewMessage(newMessage) as CreatedAtActionResult;

            //Assert
            Assert.Equal(201, action.StatusCode);
            Assert.Equal("GetIssueMessage", action.ActionName);
        }

        [Fact]
        public async Task GetIssueMessagesSuccess()
        {
            //Arrange
            var issueMessages = new List<GetIssueMessageDto>()
            {
                new GetIssueMessageDto {Content = "test"},
                new GetIssueMessageDto {Content = "test"}
            };

            messageService.Setup(x => x.GetIssueMessages(It.IsAny<int>(), true)).Returns(Task.FromResult(issueMessages));

            var controller = new MessageController(messageService.Object);

            //Act
            var action = await controller.GetIssueMessages(1, true) as OkObjectResult;
            var item = action.Value as List<GetIssueMessageDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, item.Count);
        }

        [Fact]
        public async Task GetIssueMessageFailed()
        {
            //Arrange
            messageService.Setup(x => x.GetIssueMessage(1)).Returns(Task.FromResult((GetIssueMessageDto)null));

            var controller = new MessageController(messageService.Object);

            //Act
            var action = await controller.GetIssueMessage(1) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task GetIssueMessageSuccess()
        {
            //Arrange
            var issueMessage = new GetIssueMessageDto { Id = 1, Content = "test" };

            messageService.Setup(x => x.GetIssueMessage(1)).Returns(Task.FromResult(issueMessage));

            var controller = new MessageController(messageService.Object);

            //Act
            var action = await controller.GetIssueMessage(1) as OkObjectResult;
            var item = action.Value as GetIssueMessageDto;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal("test", item.Content);
        }
    }
}
