using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.Core.Models
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
