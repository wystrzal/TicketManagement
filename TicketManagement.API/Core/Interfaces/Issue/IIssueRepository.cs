using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;

namespace TicketManagement.API.Core.Interfaces
{
    public interface IIssueRepository
    {
        Task<List<Issue>> GetIssues(Func<Issue, bool> specification, int pageIndex, int pageSize);
        Task<List<Issue>> GetIssuesWithContent(Func<Issue, bool> content, Func<Issue, bool> specification, int pageIndex, int pageSize);
        Task<int> CountIssues(Func<Issue, bool> specification);
        Task<int> CountIssuesWithContent(Func<Issue, bool> content, Func<Issue, bool> specification);
    }

}
