using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models.Enums
{
    public class IssuePriority
    {
        public enum Priority
        {
            Lack = 0,
            Low = 1,
            Medium = 2,
            High = 3,
            VeryHigh = 4
        }
    }
}
