using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models.Enums
{
    public class TypeOfSearch
    {
        public enum SearchFor
        {
            UserIssues = 1,
            SupportIssues = 2,
            AllIssues = 3
        }
    }
}
