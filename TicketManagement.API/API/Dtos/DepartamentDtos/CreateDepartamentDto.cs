using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.API.API.Dtos.AccountDtos
{
    public class CreateDepartamentDto
    {
        [Required]
        public string Name { get; set; }
    }
}
