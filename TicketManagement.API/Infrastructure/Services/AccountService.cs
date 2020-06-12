using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.AccountDtos;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Dtos.AccountDtos;

namespace TicketManagement.API.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ITokenService tokenService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        public AccountService(ITokenService tokenService, UserManager<User> userManager,
            SignInManager<User> signInManager, IMapper mapper)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<string> TryLogin(LoginDto loginDto)
        {
            var dbUser = await userManager.FindByNameAsync(loginDto.Username);

            if (dbUser == null)
            {
                return null;
            }

            var result = await signInManager.CheckPasswordSignInAsync(dbUser, loginDto.Password, false);

            if (result.Succeeded)
            {
                return await tokenService.GenerateToken(dbUser, userManager);
            }

            return null;
        }

        public async Task<bool> AddUser(RegisterDto registerDto)
        {
            var userToCreate = mapper.Map<User>(registerDto);

            var result = await userManager.CreateAsync(userToCreate, registerDto.Password);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
