using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Dtos.AccountDtos;

namespace TicketManagement.API.Core.Interfaces
{
    public interface IAccountService
    {
        Task<string> TryLogin(LoginDto loginDto);
        Task<bool> AddUser(RegisterDto registerDto);
    }
}
