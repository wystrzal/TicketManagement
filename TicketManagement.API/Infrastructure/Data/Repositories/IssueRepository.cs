using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos;

namespace TicketManagement.API.Infrastructure.Data.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly DataContext dataContext;

        public IssueRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<int> CountIssues(Func<Issue, bool> func)
        {
            int totalIssues = dataContext.Issues.Where(func).Count();

            return await Task.FromResult(totalIssues);
        }

        public async Task<List<Issue>> GetIssues(Func<Issue, bool> func, int pageIndex, int pageSize)
        {
            List<Issue> issues = dataContext.Issues.Where(func).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return await Task.FromResult(issues);
        }
    }
}
