using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.API.Controllers;
using TicketManagement.API.API.Dtos.AccountDtos;
using TicketManagement.API.Core.Interfaces.DepartamentInterfaces;
using TicketManagement.API.Core.Models;
using Xunit;

namespace TicketManagement.API_TEST.Controllers
{
    public class DepartamentControllerTest
    {
        private readonly Mock<IDepartamentService> departamentService;

        public DepartamentControllerTest()
        {
            departamentService = new Mock<IDepartamentService>();
        }

        [Fact]
        public async Task CreateDepartamentModelIsNotValid()
        {
            //Arrange
            var createDepartament = new CreateDepartamentDto();

            var controller = new DepartamentController(departamentService.Object);

            //Act
            var action = await controller.CreateDepartament(createDepartament) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CreateDepartamentBadRequestResponse()
        {
            //Arrange
            var createDepartament = new CreateDepartamentDto { Name = "test" };

            departamentService.Setup(x => x.AddDepartament(createDepartament)).Returns(Task.FromResult(false));

            var controller = new DepartamentController(departamentService.Object);

            //Act
            var action = await controller.CreateDepartament(createDepartament) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task CreateDepartamentOkResponse()
        {
            //Arrange
            var createDepartament = new CreateDepartamentDto { Name = "test" };

            departamentService.Setup(x => x.AddDepartament(createDepartament)).Returns(Task.FromResult(true));

            var controller = new DepartamentController(departamentService.Object);

            //Act
            var action = await controller.CreateDepartament(createDepartament) as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task GetDepartamentsOkObjectResponse()
        {
            //Arrange
            var departaments = GetDepartaments();

            departamentService.Setup(x => x.GetDepartaments()).Returns(Task.FromResult(departaments));

            var controller = new DepartamentController(departamentService.Object);

            //Act
            var action = await controller.GetDepartaments() as OkObjectResult;
            var item = action.Value as List<GetDepartamentDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, item.Count);
        }

        private List<GetDepartamentDto> GetDepartaments()
        {
            return new List<GetDepartamentDto>
            {
                new GetDepartamentDto { Id = 1, Name = "test" },
                new GetDepartamentDto { Id = 2, Name = "test" }
            };
        }
    }
}
