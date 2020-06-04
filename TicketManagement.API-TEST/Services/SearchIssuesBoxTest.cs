using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;
using Xunit;

namespace TicketManagement.API_TEST.Services
{
    public class SearchIssuesBoxTest
    {
        private readonly Mock<IIssueRepository> issueRepository;

        public SearchIssuesBoxTest()
        {
            issueRepository = new Mock<IIssueRepository>();
        }

        [Fact]
        public void ConcreteSearchTest()
        {
            //Arrange
            var type = typeof(SearchIssuesByStatus);
            var searchSpecification = new SearchSpecificationDto();

            var service = new SearchIssuesBox(issueRepository.Object);

            //Act
            var action = service.ConcreteSearch(type, searchSpecification);

            //Assert
            Assert.Equal(type, action.GetType());
        }
    }
}
