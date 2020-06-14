using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;
using Xunit;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API_TEST.Services
{
    public class SearchSpecificationBoxTest
    {
        private readonly Mock<ISearchBy> searchBy;

        public SearchSpecificationBoxTest()
        {
            searchBy = new Mock<ISearchBy>();
        }

        [Fact]
        public void ConcreteSearchTest()
        {
            //Arrange
            var type = typeof(SearchIssuesByStatus);
            var searchSpecification = new SearchSpecificationDto();

            var service = new SearchSpecificationBox(searchBy.Object);

            //Act
            try
            {
                service.ConcreteSearch(type, searchSpecification);
                return;
            }
            catch (Exception)
            {
                //Assert
                Assert.False(true);        
            }
        }

        [Fact]
        public async Task SearchTestAsync()
        {
            //Arrange
            var searchSpecification = new SearchSpecificationDto();
            var service = new SearchSpecificationBox(searchBy.Object);

            var issueCount = new IssueCount() { FilteredIssue = 2 };
            var filteredIssueList = new FilteredIssueListDto()
            {
                Count = issueCount
            };

            searchBy.Setup(x => x.SearchIssues(x => x.Id != 0, It.IsAny<Expression<Func<Issue,bool>>>(), searchSpecification))
                .Returns(Task.FromResult(filteredIssueList));

            //Act
            var action = await service.Search(x => x.Id != 0, searchSpecification);

            //Assert
            Assert.Equal(2, action.Count.FilteredIssue);
        }
    }
}
