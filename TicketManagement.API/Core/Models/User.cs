using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Departament Departament { get; set; }

        public virtual ICollection<Issue> DeclarantedIssues { get; set; }
        public virtual ICollection<SupportIssues> SupportIssues { get; set; }
    }
}
