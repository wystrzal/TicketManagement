using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;

namespace TicketManagement.API.Core.Interfaces
{
    public interface IIssueRepository
    {
        Task<List<Issue>> GetIssues(Func<Issue, bool> func, int pageIndex, int pageSize);
        Task<int> CountIssues(Func<Issue, bool> func);
    }

}
