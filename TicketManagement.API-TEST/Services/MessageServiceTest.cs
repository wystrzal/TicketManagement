using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.MessageDtos;
using TicketManagement.API.Infrastructure.Services;
using Xunit;

namespace TicketManagement.API_TEST.Services
{
    public class MessageServiceTest
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<IMapper> mapper;
        public MessageServiceTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AddNewMessageFailed()
        {
            //Arrange
            var message = new Message { Id = 1, Content = "test" };
            var newMessage = new NewMessageDto { Content = "test" };

            mapper.Setup(x => x.Map<Message>(newMessage)).Returns(message);

            unitOfWork.Setup(x => x.Repository<Message>().Add(message)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(false));

            var service = new MessageService(unitOfWork.Object, mapper.Object);

            //Act
            var action = await service.AddNewMessage(newMessage);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public async Task AddNewMessageSuccess()
        {
            //Arrange
            var message = new Message { Id = 1, Content = "test" };
            var newMessage = new NewMessageDto { Content = "test" };

            mapper.Setup(x => x.Map<Message>(newMessage)).Returns(message);

            unitOfWork.Setup(x => x.Repository<Message>().Add(message)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            var service = new MessageService(unitOfWork.Object, mapper.Object);

            //Act
            var action = await service.AddNewMessage(newMessage);

            //Assert
            Assert.True(action);
        }

        [Fact]
        public async Task GetIssueMessagesSuccess()
        {
            //Arrange
            int issueId = 1;

            var messages = new List<Message>()
            {
                new Message {Id = 1, Content = "test"},
                new Message {Id = 2, Content = "test"}
            };

            var getIssueMessages = new List<GetIssueMessageDto>
            {
                new GetIssueMessageDto {Content = "test"},
                new GetIssueMessageDto {Content = "test"}
            };

            unitOfWork.Setup(x => x.Repository<Message>()
                .GetByConditionWithIncludeToList(It.IsAny<Func<Message, bool>>(), It.IsAny<Expression<Func<Message,User>>>()))
                .Returns(Task.FromResult(messages));

            mapper.Setup(x => x.Map<List<GetIssueMessageDto>>(messages)).Returns(getIssueMessages);

            var service = new MessageService(unitOfWork.Object, mapper.Object);

            //Act
            var action = await service.GetIssueMessages(issueId);

            //Assert
            Assert.Equal(2, action.Count);
        }

        [Fact]
        public async Task GetMessageSuccess()
        {
            //Arrange
            var messageId = 1;
            var message = new Message { Id = 1, Content = "test" };

            var service = new MessageService(unitOfWork.Object, mapper.Object);

            unitOfWork.Setup(x => x.Repository<Message>().GetById(messageId)).Returns(Task.FromResult(message));

            //Act
            var action = await service.GetIssueMessage(messageId);

            //Arrange
            Assert.NotNull(action);
            Assert.Equal("test", action.Content);
        }

    }
}
