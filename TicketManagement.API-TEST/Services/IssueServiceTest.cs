using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.IssueDtos;
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
        private readonly Mock<ISearchSpecificationBox> searchIssuesBox;

        public IssueServiceTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            mapper = new Mock<IMapper>();
            searchIssuesBox = new Mock<ISearchSpecificationBox>();
        }

        [Fact]
        public async Task AddNewIssueFailed()
        {
            //Arrange
            var issue = GetIssue();
            var newIssueDto = GetNewIssueDto();

            mapper.Setup(x => x.Map<Issue>(newIssueDto)).Returns(issue);

            unitOfWork.Setup(x => x.Repository<Issue>().Add(issue)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(false));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.AddNewIssue(newIssueDto);

            //Assert
            Assert.False(action);
            unitOfWork.Verify(x => x.Repository<Issue>().Add(issue), Times.Once);
        }

        [Fact]
        public async Task AddNewIssueSuccess()
        {
            //Arrange
            var issue = GetIssue();
            var newIssueDto = GetNewIssueDto();

            mapper.Setup(x => x.Map<Issue>(newIssueDto)).Returns(issue);

            unitOfWork.Setup(x => x.Repository<Issue>().Add(issue)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.AddNewIssue(newIssueDto);

            //Assert
            Assert.True(action);
            unitOfWork.Verify(x => x.Repository<Issue>().Add(issue), Times.Once);
        }

        [Fact]
        public async Task ChangeIssueStatusFailed()
        {
            //Arrange
            int id = 1;
            var issue = GetIssue();

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
            var issue = GetIssue();

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
        public async Task GetIssueSuccess()
        {
            //Arrange
            var issue = new Issue { Id = 1, Title = "test" };
            var getIssueDto = new GetIssueDto { Id = 1, Title = "test" };

            unitOfWork.Setup(x => x.Repository<Issue>().GetByConditionWithIncludeFirst(It.IsAny<Func<Issue,bool>>(),
                y => y.Declarant.Departament)).Returns(Task.FromResult(issue));

            mapper.Setup(x => x.Map<GetIssueDto>(issue)).Returns(getIssueDto);

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Action
            var action = await service.GetIssue(1);

            //Assert
            Assert.NotNull(action.Title);
            Assert.Equal(1, action.Id);
        }

        [Fact]
        public async Task GetIssuesByExampleType()
        {
            //Arrange
            var searchSpecification = new SearchSpecificationDto();
            var type = typeof(SearchIssuesWithoutClosed);
            var issueCount = new IssueCount { FilteredIssue = 2 };
            var filteredIssueList = new FilteredIssueListDto {Count = issueCount , Issues = GetIssueList() };

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            searchIssuesBox.Setup(x => x.ConcreteSearch(It.IsAny<Type>(), searchSpecification)).Verifiable();

            searchIssuesBox.Setup(x => x.Search(x => x.Id != 0, searchSpecification)).Returns(Task.FromResult(filteredIssueList));

            mapper.Setup(x => x.Map<List<GetIssueListDto>>(filteredIssueList.Issues)).Returns(GetIssueListDto());

            //Act
            var action = await service.GetIssues(searchSpecification);

            //Assert
            Assert.NotNull(action.Data);
            searchIssuesBox.Verify(x => x.ConcreteSearch(It.IsAny<Type>(), searchSpecification), Times.Once);
        }

        [Fact]
        public async Task GetIssueDepartamentsSuccess()
        {
            //Arrange
            var departaments = GetDepartamentList();

            var issueDepartaments = GetIssueDepartamentListDto();

            unitOfWork.Setup(x => x.Repository<Departament>().GetAll()).Returns(Task.FromResult(departaments));

            mapper.Setup(x => x.Map<List<GetIssueDepartamentDto>>(departaments)).Returns(issueDepartaments);

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.GetIssueDepartaments();

            //Assert
            Assert.NotNull(action);
            Assert.Equal(2, action.Count);
        }

        [Fact]
        public async Task GetIssueSupportSuccess()
        {
            //Arrange
            var supportIssue = GetSupportIssuesList();

            var issueSupportDto = GetIssueSupportListDto();

            unitOfWork.Setup(x => x.Repository<SupportIssues>()
            .GetByConditionWithIncludeToList(It.IsAny<Func<SupportIssues, bool>>(), y => y.User)).Returns(Task.FromResult(supportIssue));

            mapper.Setup(x => x.Map<List<GetIssueSupportDto>>(supportIssue)).Returns(issueSupportDto);

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.GetIssueSupport(1);

            //Assert
            Assert.Equal(2, action.Count);
        }

        [Fact]
        public async Task AssignToIssueFailed()
        {
            //Arrange
            var supportIssue = GetSupportIssue();

            unitOfWork.Setup(x => x.Repository<SupportIssues>().Add(supportIssue)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(false));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.AssignToIssue(1, "1");

            //Assert
            Assert.False(action);
        }

        [Fact]
        public async Task AssignToIssueSuccess()
        {
            //Arrange
            var supportIssue = GetSupportIssue();

            unitOfWork.Setup(x => x.Repository<SupportIssues>().Add(supportIssue)).Verifiable();

            unitOfWork.Setup(x => x.SaveAllAsync()).Returns(Task.FromResult(true));

            var service = new IssueService(unitOfWork.Object, mapper.Object, searchIssuesBox.Object);

            //Act
            var action = await service.AssignToIssue(1, "1");

            //Assert
            Assert.True(action);
        }



        private List<GetIssueDepartamentDto> GetIssueDepartamentListDto()
        {
            return new List<GetIssueDepartamentDto>()
            {
                new GetIssueDepartamentDto {Name = "test"},
                new GetIssueDepartamentDto {Name = "test"}
            };
        }

        private List<Departament> GetDepartamentList()
        {
            return new List<Departament>()
            {
                new Departament {Id = 1, Name = "test"},
                new Departament {Id = 2, Name = "test"}
            };
        }

        private SupportIssues GetSupportIssue()
        {
            return new SupportIssues { IssueId = 1, SupportId = "1" };
        }

        private List<GetIssueSupportDto> GetIssueSupportListDto()
        {
            return new List<GetIssueSupportDto>
            {
                new GetIssueSupportDto {SupportId = "1"},
                new GetIssueSupportDto {SupportId = "2"}
            };
        }

        private List<SupportIssues> GetSupportIssuesList()
        {
            return new List<SupportIssues>
            {
                new SupportIssues {IssueId = 1, SupportId = "1"},
                new SupportIssues {IssueId = 1, SupportId = "2"}
            };
        }

        private List<Issue> GetIssueList()
        {
            return new List<Issue>()
            {
                new Issue {Id = 1},
                new Issue {Id = 2},
            };
        }

        private Issue GetIssue()
        {
            return new Issue { Title = "test", Description = "test", DeclarantId = "1", Status = Status.New };
        }

        private NewIssueDto GetNewIssueDto()
        {
            return new NewIssueDto { Title = "test", Description = "test", DeclarantId = "1" };
        }

        private List<GetIssueListDto> GetIssueListDto()
        {
            return new List<GetIssueListDto>()
            {
                new GetIssueListDto {Id = 1},
                new GetIssueListDto {Id = 2},
            };
        }
    }
}
