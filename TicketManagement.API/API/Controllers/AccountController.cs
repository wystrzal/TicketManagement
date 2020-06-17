using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.API.API.Dtos.AccountDtos;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Dtos.AccountDtos;
using TicketManagement.API.Extensions;

namespace TicketManagement.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        //TEST
        [HttpGet("{departament}")]
        public async Task<IActionResult> GetUsers(string departament)
        {
            return Ok(await accountService.GetUsers(departament));
        }

        //TEST
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (await accountService.DeleteUser(userId))
            {
                return Ok();
            }

            return BadRequest("Something goes wrong.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            string token = await accountService.TryLogin(loginDto);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(RegisterDto registerDto)
        {
            if (registerDto.Password != registerDto.RepeatPassword)
            {
                return BadRequest("The password repeat is incorrect.");
            }

            if (ModelState.IsValid && registerDto.Password.ContainsDigit()
                && registerDto.Password.ContainsUpper())
            {
                if (await accountService.AddUser(registerDto))
                {
                    return Ok();
                }

                return BadRequest("User with this login already exists.");
            }

            return BadRequest("Password must have minimum 6 signs (1 digit, 1 uppercase letter).");
        }

    }
}