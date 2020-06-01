using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models.Enums
{
    public class IssueStatus
    {
        public enum Status
        {
            New = 1,
            Open = 2,
            Progress = 3,
            Pending = 4,
            Close = 5
        }
    }
}
