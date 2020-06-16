using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
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

        //Count all issues by specification e.g. departament + declarant lastname.
        public async Task<int> CountIssues(Func<Issue, bool> specification)
        {
            int totalIssues = dataContext.Issues.Include(x => x.Declarant).Where(specification).Count();

            return await Task.FromResult(totalIssues);
        }

        //Get specified number of issues by specification e.g. status + title.
        public async Task<List<Issue>> GetIssues(Func<Issue, bool> specification,
            int pageIndex, int pageSize)
        {
            List<Issue> issues = dataContext.Issues.Include(x => x.SupportIssues).ThenInclude(x => x.User)
                .Include(x => x.Declarant).ThenInclude(x => x.Departament)
                .Where(specification).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return await Task.FromResult(issues);
        }
    }
}
