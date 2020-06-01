using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Core.Models.Enums;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssueStrategy;
using TicketManagement.API.Infrastructure.Services.SearchIssueStrategy.ConcreteSearch;
using static TicketManagement.API.Core.Models.Enums.IssueStatus;

namespace TicketManagement.API.Infrastructure.Services
{
    public class IssueService : IIssueService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ISearchIssuesBox searchIssuesBox;

        public IssueService(IUnitOfWork unitOfWork, IMapper mapper, ISearchIssuesBox searchIssuesBox)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.searchIssuesBox = searchIssuesBox;
        }

        public async Task<bool> AddNewIssue(NewIssueDto newIssue)
        {
            var issueToAdd = mapper.Map<Issue>(newIssue);

            issueToAdd.Status = Status.New;

            unitOfWork.Repository<Issue>().Add(issueToAdd);

            if (await unitOfWork.SaveAllAsync())
            {
                return true;
            }

            return false;
        }

        public async Task<PaginatedItemsDto<Issue>> GetIssues(SearchSpecificationDto searchSpecification)
        {
            PaginatedItemsDto<Issue> paginatedItems = null;

            if (searchSpecification.Status != null)
            {
                paginatedItems = await searchIssuesBox.SearchIssues<SearchIssuesByStatus>()
                    .SearchIssues(searchSpecification);
            }

            return paginatedItems;
        }

    }
}
