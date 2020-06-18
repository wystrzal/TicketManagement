using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.AccountDtos;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Infrastructure.Services;
using Xunit;

namespace TicketManagement.API_TEST.Services
{
    public class DepartamentServiceTest
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        public DepartamentServiceTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task AddDepartamentSuccess()
        {
            //Arrange
            var createDepartament = new CreateDepartamentDto { Name = "test" };
            var departament = new Departament { Name = "test" };

            unitOfWork.Setup(x => x.Mapper().Map<Departament>(createDepartament)).Returns(departament);

            unitOfWork.Setup(x => x.Repository<Departament>().Add(departament)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            var service = new DepartamentService(unitOfWork.Object);

            //Act
            var action = await service.AddDepartament(createDepartament);

            //Assert
            Assert.True(action);
            unitOfWork.Verify(x => x.Repository<Departament>().Add(departament), Times.Once);
        }

        [Fact]
        public async Task AddDepartamentFailed()
        {
            //Arrange
            var createDepartament = new CreateDepartamentDto { Name = "test" };
            var departament = new Departament { Name = "test" };

            unitOfWork.Setup(x => x.Mapper().Map<Departament>(createDepartament)).Returns(departament);

            unitOfWork.Setup(x => x.Repository<Departament>().Add(departament)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(false));

            var service = new DepartamentService(unitOfWork.Object);

            //Act
            var action = await service.AddDepartament(createDepartament);

            //Assert
            Assert.False(action);
            unitOfWork.Verify(x => x.Repository<Departament>().Add(departament), Times.Once);
        }

        [Fact]
        public async Task GetDepartamentsSuccess()
        {
            //Arrange
            var departaments = new List<Departament>
            {
                new Departament {Name = "test", Id = 1},
                new Departament {Name = "test", Id = 2}
            };

            var getDepartaments = new List<GetDepartamentDto>
            {
                new GetDepartamentDto {Name = "test"},
                new GetDepartamentDto {Name = "test"}
            };

            unitOfWork.Setup(x => x.Repository<Departament>().GetAll()).Returns(Task.FromResult(departaments));

            unitOfWork.Setup(x => x.Mapper().Map<List<GetDepartamentDto>>(departaments)).Returns(getDepartaments);

            var service = new DepartamentService(unitOfWork.Object);

            //Act
            var action = await service.GetDepartaments();

            //Arrange
            Assert.Equal(2, action.Count);
        }
    }
}
