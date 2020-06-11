using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.IssueDtos;
using TicketManagement.API.Controllers;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services;
using Xunit;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;
using static TicketManagement.API.Core.Models.Enums.TypeOfSearch;

namespace TicketManagement.API_TEST.Controllers
{
    public class IssueControllerTest
    {
        private readonly Mock<IIssueService> issueService;

        public IssueControllerTest()
        {
            issueService = new Mock<IIssueService>();
        }

        [Fact]
        public async Task AddNewIssueModelStateIsNotValid()
        {
            //Arrange
            var newIssue = new NewIssueDto();

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.AddNewIssue(newIssue) as BadRequestObjectResult;

            //Arrange
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task AddNewIssueBadRequestResponse()
        {
            //Arrange
            var newIssue = new NewIssueDto { DeclarantId = "1", Description = "test", Title = "test" };

            issueService.Setup(x => x.AddNewIssue(newIssue)).Returns(Task.FromResult(false));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.AddNewIssue(newIssue) as BadRequestObjectResult;

            //Arrange
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task AddNewIssueOkResponse()
        {
            //Arrange
            var newIssue = new NewIssueDto { DeclarantId = "1", Description = "test", Title = "test" };

            issueService.Setup(x => x.AddNewIssue(newIssue)).Returns(Task.FromResult(true));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.AddNewIssue(newIssue) as OkResult;

            //Arrange
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task ChangeIssueStatusBadRequestResponse()
        {
            //Arrange
            int id = 1;
            var status = Status.Close;

            issueService.Setup(x => x.ChangeIssueStatus(id, status)).Returns(Task.FromResult(false));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.ChangeIssueStatus(id, status) as BadRequestObjectResult;

            //Arrange
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task ChangeIssueStatusOkResponse()
        {
            //Arrange
            int id = 1;
            var status = Status.Close;

            issueService.Setup(x => x.ChangeIssueStatus(id, status)).Returns(Task.FromResult(true));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.ChangeIssueStatus(id, status) as OkResult;

            //Arrange
            Assert.Equal(200, action.StatusCode);
        }

        [Fact]
        public async Task GetIssuesOkObjectResponse()
        {
            //Arrange
            int pageIndex = 1;
            int pageSize = 2;
            int count = 2;

            var searchSpecification
                = new SearchSpecificationDto { PageIndex = pageIndex, PageSize = pageSize, SearchFor = SearchFor.AllIssues};

            issueService.Setup(x => x.GetIssues(searchSpecification))
                .Returns(Task.FromResult(GetPaginatedItems(pageIndex, count, pageSize)));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.GetIssues(searchSpecification) as OkObjectResult;
            var item = action.Value as PaginatedItemsDto<GetIssueListDto>;

            //Arrange
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, item.Data.Count());
        }

        [Fact]
        public async Task GetIssueBadRequestResponse()
        {
            //Arrange
            int id = 1;

            issueService.Setup(x => x.GetIssue(id)).Returns(Task.FromResult((GetIssueDto)null));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.GetIssue(id) as BadRequestObjectResult;

            //Arrange
            Assert.Equal(400, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task GetIssueOkObjectResponse()
        {
            //Arrange
            int id = 1;
            var issue = new GetIssueDto { Title = "test", Description = "test" };

            issueService.Setup(x => x.GetIssue(id)).Returns(Task.FromResult(issue));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.GetIssue(id) as OkObjectResult;

            //Arrange
            Assert.Equal(200, action.StatusCode);
            Assert.NotNull(action.Value);
        }

        [Fact]
        public async Task GetIssueDepartamentsOkObjectResponse()
        {
            //Arrange
            var getIssueDepartaments = new List<GetIssueDepartamentDto>()
            {
                new GetIssueDepartamentDto { Name = "test"},
                new GetIssueDepartamentDto { Name = "test"}
            };

            issueService.Setup(x => x.GetIssueDepartaments()).Returns(Task.FromResult(getIssueDepartaments));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.GetIssueDepartaments() as OkObjectResult;
            var item = action.Value as List<GetIssueDepartamentDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, item.Count);
        }

        [Fact]
        public async Task GetIssueSupportOkObjectResponse()
        {
            //Arrange 
            var issueSupport = new List<GetIssueSupportDto>
            {
                new GetIssueSupportDto {SupportId = "1"},
                new GetIssueSupportDto {SupportId = "2"}
            };

            issueService.Setup(x => x.GetIssueSupport(1)).Returns(Task.FromResult(issueSupport));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.GetIssueSupport(1) as OkObjectResult;
            var item = action.Value as List<GetIssueSupportDto>;

            //Assert
            Assert.Equal(200, action.StatusCode);
            Assert.Equal(2, item.Count);
        }

        [Fact]
        public async Task AssignToIssueBadRequestResponse()
        {
            //Arrange
            issueService.Setup(x => x.AssignToIssue(1, "1")).Returns(Task.FromResult(false));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.AssignToIssue(1, "1") as BadRequestObjectResult;

            //Assert
            Assert.NotNull(action.Value);
            Assert.Equal(400, action.StatusCode);
        }

        [Fact]
        public async Task AssignToIssueOkResponse()
        {
            //Arrange
            issueService.Setup(x => x.AssignToIssue(1, "1")).Returns(Task.FromResult(true));

            var controller = new IssueController(issueService.Object);

            //Act
            var action = await controller.AssignToIssue(1, "1") as OkResult;

            //Assert
            Assert.Equal(200, action.StatusCode);
        }


        private PaginatedItemsDto<GetIssueListDto> GetPaginatedItems(int pageIndex, int count, int pageSize)
        {
            var issueList = new List<GetIssueListDto>()
            {
                new GetIssueListDto {Status = Status.New, Declarant = "1", Id = 1, Title = "test1", Departament = "production"},
                new GetIssueListDto {Status = Status.New, Declarant = "1", Id = 2, Title = "test2", Departament = "production"},
            };

            return new PaginatedItemsDto<GetIssueListDto>(pageIndex, count, issueList, pageSize);
        }
    }
}
