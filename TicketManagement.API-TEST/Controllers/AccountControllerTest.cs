using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.AccountDtos;
using TicketManagement.API.Controllers;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Dtos.AccountDtos;
using Xunit;

namespace TicketManagement.API_TEST.Controllers
{
    public class AccountControllerTest
    {
        private readonly Mock<IAccountService> accountService;
        public AccountControllerTest()
        {
            accountService = new Mock<IAccountService>();
        }
        
        [Fact]
        public async Task LoginUnauthorizedResultResponse()
        {
            //Arrange
            var loginDto = new LoginDto { Password = "test", Username = "test" };

            accountService.Setup(x => x.TryLogin(loginDto)).Returns(Task.FromResult((string)null));

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.Login(loginDto) as UnauthorizedResult;

            //Assert
            Assert.Equal(401, action.StatusCode);
        }

        [Fact]
        public async Task LoginOkObjectResponse()
        {
            //Arrange
            var loginDto = new LoginDto { Password = "test", Username = "test" };

            accountService.Setup(x => x.TryLogin(loginDto)).Returns(Task.FromResult("very long token"));

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.Login(loginDto) as OkObjectResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CreateUserIncorrectPassword()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "test", RepeatPassword = "incorrect", Username = "test" };

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.CreateUser(registerDto) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CreateUserModelStateIsNotValid()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "Test123", RepeatPassword = "Test123" };

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.CreateUser(registerDto) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CreateUserPasswordDoesntContainsDigit()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "Testtest", RepeatPassword = "Testtest", Username = "test" };

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.CreateUser(registerDto) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CreateUserPasswordDoesntContainsUpper()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "test123", RepeatPassword = "test123", Username = "test" };

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.CreateUser(registerDto) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CreateUserUserAlreadyExist()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "Test123", RepeatPassword = "Test123", Username = "test" };

            var controller = new AccountController(accountService.Object);

            accountService.Setup(x => x.AddUser(registerDto)).Returns(Task.FromResult(false));

            //Act
            var action = await controller.CreateUser(registerDto) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CreateUserOkResponse()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "Test123", RepeatPassword = "Test123", Username = "test" };

            var controller = new AccountController(accountService.Object);

            accountService.Setup(x => x.AddUser(registerDto)).Returns(Task.FromResult(true));

            //Act
            var action = await controller.CreateUser(registerDto) as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task DeleteUserOkResponse()
        {
            //Arrange
            string userId = "1";

            accountService.Setup(x => x.DeleteUser(userId)).Returns(Task.FromResult(true));

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.DeleteUser(userId) as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task DeleteUserBadRequestResponse()
        {
            //Arrange
            string userId = "1";

            accountService.Setup(x => x.DeleteUser(userId)).Returns(Task.FromResult(false));

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.DeleteUser(userId) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task GetUsersOkObjectResponse()
        {
            //Arrange
            string departament = "all";

            var users = GetUsers();

            accountService.Setup(x => x.GetUsers(departament)).Returns(Task.FromResult(users));

            var controller = new AccountController(accountService.Object);

            //Act
            var action = await controller.GetUsers(departament) as OkObjectResult;
            var item = action.Value as List<GetUserDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, item.Count);
        }

        private List<GetUserDto> GetUsers()
        {
            return new List<GetUserDto>
            {
                new GetUserDto {Firstname = "test"},
                new GetUserDto {Firstname = "test"}
            };
        }
    }
}
