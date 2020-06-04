using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;
using Xunit;

namespace TicketManagement.API_TEST.Services
{
    public class SearchByTest
    {
        private readonly Mock<IIssueRepository> issueRepository;
        private readonly SearchSpecificationDto searchSpecification;
        public SearchByTest()
        {
            issueRepository = new Mock<IIssueRepository>();
            searchSpecification = new SearchSpecificationDto();
        }

        [Fact]
        public async Task SearchIssuesTest()
        {
            //Arrange
            var issues = new List<Issue>()
            {
                new Issue {Id = 1},
                new Issue {Id = 2}
            };

            int totalIssues = 2;

            var service = new SearchIssuesByDepartament(issueRepository.Object, searchSpecification);

            issueRepository.Setup(x => x.GetIssues(It.IsAny<Func<Issue,bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(issues));

            issueRepository.Setup(x => x.CountIssues(It.IsAny<Func<Issue, bool>>())).Returns(Task.FromResult(totalIssues));

            //Act
            var action = await service.SearchIssues(x => x.Id != 0);


            //Assert
            Assert.Equal(totalIssues, action.Issues.Count);
            Assert.Equal(totalIssues, action.totalIssues);
        }

        [Fact]
        public async Task SearchByContentTest()
        {
            //Arrange
            var issues = new List<Issue>()
            {
                new Issue {Id = 1},
                new Issue {Id = 2}
            };

            int totalIssues = 2;

            var service = new SearchIssuesByDepartament(issueRepository.Object, searchSpecification);

            issueRepository.Setup(x => x.GetIssues(It.IsAny<Func<Issue, bool>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(issues));

            issueRepository.Setup(x => x.CountIssues(It.IsAny<Func<Issue, bool>>())).Returns(Task.FromResult(totalIssues));

            //Act
            var action = await service.SearchByContent(x => x.Id != 0);

            //Assert
            Assert.Equal(totalIssues, action.Issues.Count);
            Assert.Equal(totalIssues, action.totalIssues);
        }
    }
}
