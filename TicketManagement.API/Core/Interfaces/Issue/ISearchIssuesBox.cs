using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Interfaces
{
    public interface ISearchIssuesBox
    {
        ISearchIssues SearchIssues<T>() where T : class;
    }
}
