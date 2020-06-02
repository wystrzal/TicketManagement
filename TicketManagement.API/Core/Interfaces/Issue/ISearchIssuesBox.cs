using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Infrastructure.Services.SearchIssue.ConcreteSearch;

namespace TicketManagement.API.Core.Interfaces
{
    public interface ISearchIssuesBox
    {
        SearchByAbstract SearchIssues<T>() where T : class;
    }
}
