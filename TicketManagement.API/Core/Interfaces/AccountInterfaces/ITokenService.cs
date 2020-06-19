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
        /// <summary>
        /// Generate token needed for user authorization.
        /// </summary>
        Task<string> GenerateToken(User user, UserManager<User> userManager);
    }
}
