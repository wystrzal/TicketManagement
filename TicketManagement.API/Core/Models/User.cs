using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public int DepartamentId { get; set; }
        public Departament Departament { get; set; }

        public virtual ICollection<Issue> DeclarantedIssues { get; set; }
        public virtual ICollection<SupportIssues> SupportIssues { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
