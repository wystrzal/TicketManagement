using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.AccountDtos;
using TicketManagement.API.Infrastructure.Services;
using Xunit;

namespace TicketManagement.API_TEST.Services
{
    public class AccountServiceTest
    {
        private Mock<UserManager<User>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<User>>();

            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private Mock<SignInManager<User>> GetMockSignInManager()
        {
            var _mockUserManager = new Mock<UserManager<User>>(new Mock<IUserStore<User>>().Object,
                null, null, null, null, null, null, null, null);
            var _contextAccessor = new Mock<IHttpContextAccessor>();
            var _userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            return new Mock<SignInManager<User>>(_mockUserManager.Object,
                           _contextAccessor.Object, _userPrincipalFactory.Object, null, null, null, null);
        }

        private readonly Mock<ITokenService> tokenService;
        private readonly Mock<IUnitOfWork> unitOfWork;

        public AccountServiceTest()
        {
            tokenService = new Mock<ITokenService>();
            unitOfWork = new Mock<IUnitOfWork>(); 
        }

        [Fact]
        public async Task TryLoginNullUser()
        {
            //Arrange
            var loginDto = new LoginDto { Password = "test", Username = "test" };
            var userManager = GetMockUserManager();

            userManager.Setup(x => x.FindByNameAsync(loginDto.Username)).Returns(Task.FromResult((User)null));

            var service = new AccountService(tokenService.Object, userManager.Object,
                GetMockSignInManager().Object, unitOfWork.Object);

            //Act
            var action = await service.TryLogin(loginDto);

            //Assert
            Assert.Null(action);
        }

        [Fact]
        public async Task TryLoginIncorrectPassword()
        {
            //Arrange
            var loginDto = new LoginDto { Password = "test", Username = "test" };
            var user = new User { UserName = "test" };
            var userManager = GetMockUserManager();
            var signInManager = GetMockSignInManager();

            userManager.Setup(x => x.FindByNameAsync(loginDto.Username)).Returns(Task.FromResult(user));

            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, loginDto.Password, false))
                .Returns(Task.FromResult(SignInResult.Failed));

            var service = new AccountService(tokenService.Object, userManager.Object,
                signInManager.Object, unitOfWork.Object);

            //Act
            var action = await service.TryLogin(loginDto);

            //Assert
            Assert.Null(action);
        }

        [Fact]
        public async Task TryLoginReturnToken()
        {
            //Arrange
            var loginDto = new LoginDto { Password = "test", Username = "test" };
            var user = new User { UserName = "test" };
            var userManager = GetMockUserManager();
            var signInManager = GetMockSignInManager();

            userManager.Setup(x => x.FindByNameAsync(loginDto.Username)).Returns(Task.FromResult(user));

            signInManager.Setup(x => x.CheckPasswordSignInAsync(user, loginDto.Password, false))
                .Returns(Task.FromResult(SignInResult.Success));

            tokenService.Setup(x => x.GenerateToken(user, userManager.Object)).Returns(Task.FromResult("very long token"));

            var service = new AccountService(tokenService.Object, userManager.Object,
                signInManager.Object, unitOfWork.Object);

            //Act
            var action = await service.TryLogin(loginDto);

            //Assert
            Assert.NotNull(action);
        }

        [Fact]
        public async Task AddUserFailed()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "test", RepeatPassword = "test", Username = "test" };
            var user = new User { UserName = "test" };
            var userManager = GetMockUserManager();

            unitOfWork.Setup(x => x.Mapper().Map<User>(registerDto)).Returns(user);

            userManager.Setup(x => x.CreateAsync(user, registerDto.Password)).Returns(Task.FromResult(IdentityResult.Failed()));

            var service = new AccountService(tokenService.Object, userManager.Object,
                GetMockSignInManager().Object, unitOfWork.Object);

            //Act
            var action = await service.AddUser(registerDto);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public async Task AddUserSuccess()
        {
            //Arrange
            var registerDto = new RegisterDto { Password = "test", RepeatPassword = "test", Username = "test" };
            var user = new User { UserName = "test" };
            var userManager = GetMockUserManager();

            unitOfWork.Setup(x => x.Mapper().Map<User>(registerDto)).Returns(user);

            userManager.Setup(x => x.CreateAsync(user, registerDto.Password)).Returns(Task.FromResult(IdentityResult.Success));

            var service = new AccountService(tokenService.Object, userManager.Object,
                GetMockSignInManager().Object, unitOfWork.Object);

            //Act
            var action = await service.AddUser(registerDto);

            //Assert
            Assert.True(action);
        }
    }
}
