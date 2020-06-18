using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IUnitOfWork unitOfWork;

        public AccountService(ITokenService tokenService, UserManager<User> userManager,
            SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.unitOfWork = unitOfWork;
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
            var userToCreate = unitOfWork.Mapper().Map<User>(registerDto);

            var result = await userManager.CreateAsync(userToCreate, registerDto.Password);

            if (result.Succeeded)
            {
                if (registerDto.IsAdmin)
                {
                    await userManager.AddClaimAsync(userToCreate, new Claim(ClaimTypes.Role, "admin"));
                }
                else
                {
                    await userManager.AddClaimAsync(userToCreate, new Claim(ClaimTypes.Role, "user"));
                }

                return true;
            }

            return false;
        }

        public async Task<List<GetUserDto>> GetUsers(string departament)
        {
            List<User> users = null;

            if (departament == "all")
            {
                users = await userManager.Users.Include(x => x.Departament).ToListAsync();
            }
            else
            {
                users = await userManager.Users.Include(x => x.Departament)
                    .Where(x => x.Departament.Name == departament).ToListAsync();
            }

            return unitOfWork.Mapper().Map<List<GetUserDto>>(users);
        }

        public async Task<bool> DeleteUser(string userId)
        {
            var user = await userManager.Users.Include(x => x.SupportIssues).Include(x => x.Messages)
                .Where(x => x.Id == userId).FirstOrDefaultAsync();

            if (user.SupportIssues.Count() > 0)
            {
                var supportedIssues = await unitOfWork.Repository<SupportIssues>()
                     .GetByConditionToList(x => x.SupportId == user.Id);

                Parallel.ForEach(supportedIssues, supportIssue =>
                {
                    unitOfWork.Repository<SupportIssues>().Delete(supportIssue);
                });            
            }

            if (user.Messages.Count() > 0)
            {
                var messages = await unitOfWork.Repository<Message>()
                    .GetByConditionToList(x => x.SenderId == user.Id);

                Parallel.ForEach(messages, message =>
                {
                    unitOfWork.Repository<Message>().Delete(message);
                });
            }
            await unitOfWork.SaveAllAsync();

            var deleteUser = await userManager.DeleteAsync(user);

            if (deleteUser.Succeeded)
            {
                return true;
            }

            return false;
        }
    }
}
