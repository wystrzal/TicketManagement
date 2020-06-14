using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;

namespace TicketManagement.API.Core.Interfaces.IssueInterfaces
{
    public interface IConcreteSearch
    {
        Predicate<Issue> getSpecification();
    }
}
