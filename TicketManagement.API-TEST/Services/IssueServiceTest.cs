using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;
using Xunit;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API_TEST.Services
{
    public class IssueServiceTest
    {
        private readonly Mock<IUnitOfWork> unitOfWork;
        private readonly Mock<IMapper> mapper;
        private readonly Mock<ISearchIssuesBox> searchIssuesBox;

        public IssueServiceTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            searchIssuesBox = new Mock<ISearchIssuesBox>();
        }

        [Fact]
        public async Task AddNewIssueFailed()
        {
            //Arrange
            var newIssue = new NewIssueDto { Title = "test", Description = "test", DeclarantId = "1" };
            var issue = new Issue { Title = "test", Description = "test", DeclarantId = "1" };

            mapper.Setup(x => x.Map<Issue>(newIssue)).Returns(issue);

            unitOfWork.Setup(x => x.Repository<Issue>().Add(issue)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(false));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.AddNewIssue(newIssue);

            //Assert
            Assert.False(action);
            unitOfWork.Verify(x => x.Repository<Issue>().Add(issue), Times.Once);
        }

        [Fact]
        public async Task AddNewIssueSuccess()
        {
            //Arrange
            var newIssue = new NewIssueDto { Title = "test", Description = "test", DeclarantId = "1" };
            var issue = new Issue { Title = "test", Description = "test", DeclarantId = "1" };

            mapper.Setup(x => x.Map<Issue>(newIssue)).Returns(issue);

            unitOfWork.Setup(x => x.Repository<Issue>().Add(issue)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.AddNewIssue(newIssue);

            //Assert
            Assert.True(action);
            unitOfWork.Verify(x => x.Repository<Issue>().Add(issue), Times.Once);
        }

        [Fact]
        public async Task ChangeIssueStatusFailed()
        {
            //Arrange
            int id = 1;
            var issue = new Issue {Id = id, DeclarantId = "1", Description = "test", Title = "test", Status = Status.New };

            unitOfWork.Setup(x => x.Repository<Issue>().GetById(id)).Returns(Task.FromResult(issue));

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(false));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.ChangeIssueStatus(id, Status.Close);

            //Assert
            Assert.False(action);
        }

        [Fact]
        public async Task ChangeIssueStatusSuccess()
        {
            //Arrange
            int id = 1;
            var issue = new Issue { Id = id, Status = Status.New };

            unitOfWork.Setup(x => x.Repository<Issue>().GetById(id)).Returns(Task.FromResult(issue));

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.ChangeIssueStatus(id, Status.Close);

            //Assert
            Assert.True(action);
            Assert.Equal(Status.Close, issue.Status);
        }

        [Fact]
        public async Task GetIssueNull()
        {
            //Arrange
            int id = 1;

            unitOfWork.Setup(x => x.Repository<Issue>().GetById(id)).Returns(Task.FromResult((Issue)null));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.GetIssue(id);

            //Assert
            Assert.Null(action);
        }

        [Fact]
        public async Task GetIssueSuccess()
        {
            //Arrange
            int id = 1;
            var issue = new Issue { Id = id };
            var getIssueDto = new GetIssueDto { Id = id };

            unitOfWork.Setup(x => x.Repository<Issue>().GetById(id)).Returns(Task.FromResult(issue));

            mapper.Setup(x => x.Map<GetIssueDto>(issue)).Returns(getIssueDto);

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Action
            var action = await service.GetIssue(id);

            //Assert
            Assert.NotNull(action);
            Assert.Equal(id, action.Id);
        }

        [Fact]
        public async Task GetIssuesWithoutClosed()
        {
            //Arrange
            var searchSpecification = new SearchSpecificationDto();
            var type = typeof(SearchIssuesWithoutClosed);
            var filteredIssueList = new FilteredIssueListDto { totalIssues = 3, Issues = GetIssues() };

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            searchIssuesBox.Setup(x => x.ConcreteSearch(type, searchSpecification).SearchIssues(x => x.Id != 0))
                .Returns(Task.FromResult(filteredIssueList));

            mapper.Setup(x => x.Map<List<GetIssueListDto>>(filteredIssueList.Issues)).Returns(GetIssuesDto());

            //Act
            var action = await service.GetIssues(searchSpecification);

            //Assert
            Assert.NotNull(action.Data);
        }

        [Fact]
        public async Task GetIssuesByStatusDepartament()
        {
            //Arrange
            var searchSpecification = new SearchSpecificationDto() { Status = Status.New, Departament = "test" };
            var type = typeof(SearchIssuesByStatusDepartament);
            var filteredIssueList = new FilteredIssueListDto { totalIssues = 2, Issues = GetIssues() };

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            searchIssuesBox.Setup(x => x.ConcreteSearch(type, searchSpecification).SearchIssues(x => x.Id != 0))
                .Returns(Task.FromResult(filteredIssueList));

            mapper.Setup(x => x.Map<List<GetIssueListDto>>(filteredIssueList.Issues)).Returns(GetIssuesDto());

            //Act
            var action = await service.GetIssues(searchSpecification);

            //Assert
            Assert.NotNull(action);
            Assert.NotNull(action.Data);
        }

        [Fact]
        public async Task GetIssuesByStatus()
        {
            //Arrange
            var searchSpecification = new SearchSpecificationDto() { Status = Status.New };
            var type = typeof(SearchIssuesByStatus);
            var filteredIssueList = new FilteredIssueListDto { totalIssues = 2, Issues = GetIssues() };

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            searchIssuesBox.Setup(x => x.ConcreteSearch(type, searchSpecification).SearchIssues(x => x.Id != 0))
                .Returns(Task.FromResult(filteredIssueList));

            mapper.Setup(x => x.Map<List<GetIssueListDto>>(filteredIssueList.Issues)).Returns(GetIssuesDto());

            //Act
            var action = await service.GetIssues(searchSpecification);

            //Assert
            Assert.NotNull(action);
            Assert.NotNull(action.Data);
        }

        [Fact]
        public async Task GetIssuesByDepartament()
        {
            //Arrange
            var searchSpecification = new SearchSpecificationDto() { Departament = "test" };
            var type = typeof(SearchIssuesByDepartament);
            var filteredIssueList = new FilteredIssueListDto { totalIssues = 2, Issues = GetIssues() };

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            searchIssuesBox.Setup(x => x.ConcreteSearch(type, searchSpecification).SearchIssues(x => x.Id != 0))
                .Returns(Task.FromResult(filteredIssueList));

            mapper.Setup(x => x.Map<List<GetIssueListDto>>(filteredIssueList.Issues)).Returns(GetIssuesDto());

            //Act
            var action = await service.GetIssues(searchSpecification);

            //Assert
            Assert.NotNull(action);
            Assert.NotNull(action.Data);
        }

        [Fact]
        public async Task GetIssueDepartamentsSuccess()
        {
            //Arrange
            var departaments = new List<Departament>()
            {
                new Departament {Id = 1, Name = "test"},
                new Departament {Id = 2, Name = "test"}
            };

            var issueDepartaments = new List<GetIssueDepartamentsDto>()
            {
                new GetIssueDepartamentsDto {Name = "test"},
                new GetIssueDepartamentsDto {Name = "test"}
            };

            unitOfWork.Setup(x => x.Repository<Departament>().GetAll()).Returns(Task.FromResult(departaments));

            mapper.Setup(x => x.Map<List<GetIssueDepartamentsDto>>(departaments)).Returns(issueDepartaments);

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.GetIssueDepartaments();

            //Assert
            Assert.NotNull(action);
            Assert.Equal(2, action.Count);
        }

        private List<Issue> GetIssues()
        {
            return new List<Issue>()
            {
                new Issue {Id = 1},
                new Issue {Id = 2},
            };
        }

        private List<GetIssueListDto> GetIssuesDto()
        {
            return new List<GetIssueListDto>()
            {
                new GetIssueListDto {Id = 1},
                new GetIssueListDto {Id = 2},
            };
        }
    }
}
