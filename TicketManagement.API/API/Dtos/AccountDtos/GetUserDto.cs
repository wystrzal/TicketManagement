using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.API.Dtos.AccountDtos
{
    public class GetUserDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string DepartamentName { get; set; }
        public string Id { get; set; }

    }
}
