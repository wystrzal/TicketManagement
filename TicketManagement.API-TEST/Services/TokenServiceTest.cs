using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Infrastructure.Services;
using Xunit;

namespace TicketManagement.API_TEST.Services
{
    public class TokenServiceTest
    {
        private Mock<UserManager<User>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();

            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public void GenerateTokenWriteToken()
        {
            //Arrange
            var configurationSection = new Mock<IConfigurationSection>();
            var configMock = new Mock<IConfiguration>();
            var user = new User { UserName = "test"};
            var userManager = GetMockUserManager();

            configurationSection.Setup(a => a.Value).Returns("VeryLongKeyForTest");
            configMock.Setup(a => a.GetSection("AppSettings:Token")).Returns(configurationSection.Object);

            var service = new TokenService(configMock.Object);

            //Act
            var action = service.GenerateToken(user, userManager.Object);

            //Arrange
            Assert.NotNull(action);
        }
    }
}
