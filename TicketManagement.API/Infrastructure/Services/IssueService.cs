using AutoMapper;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;
using TicketManagement.API.Dtos.IssueDtos;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;
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

        public async Task<PaginatedItemsDto<GetIssueListDto>> GetIssues(SearchSpecificationDto searchSpecification)
        {
            if (searchSpecification.Status != null && searchSpecification.Departament != null)
            {
                return await searchIssuesBox.SearchIssues<SearchIssuesByStatusDepartament>()
                    .SearchIssues(searchSpecification);
            }
            else if (searchSpecification.Status != null)
            {
                return await searchIssuesBox.SearchIssues<SearchIssuesByStatus>()
                    .SearchIssues(searchSpecification);
            } 
            else if (searchSpecification.Departament != null)
            {
                return await searchIssuesBox.SearchIssues<SearchIssuesByDepartament>()
                    .SearchIssues(searchSpecification);
            }
            else
            {
                return await searchIssuesBox.SearchIssues<SearchIssuesWithoutClosed>()
                    .SearchIssues(searchSpecification);
            }
        }

    }
}
