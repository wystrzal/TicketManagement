using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;

namespace TicketManagement.API.Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user, UserManager<User> userManager);
    }
}
