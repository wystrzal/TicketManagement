﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;

namespace TicketManagement.API.Infrastructure.Data
{
    public static class DataSeed
    {
        public static void AddSeed(UserManager<User> userManager, DataContext dataContext)
        {
            if (!userManager.Users.Any())
            {
                List<Departament> departaments = new List<Departament>()
                {
                    new Departament {Name = "Warehouse"},
                    new Departament {Name = "Production"},
                    new Departament {Name = "Support"}
                };

                dataContext.Departaments.AddRange(departaments);
                dataContext.SaveChangesAsync();


                List<User> user = new List<User>()
                {
                    new User {UserName = "boss", Departament = departaments[2], Firstname = "Patryk", Lastname = "Gruszka"},
                    new User { UserName = "admin", Departament = departaments[2], Firstname = "Patryk", Lastname = "Kowal"},
                    new User { UserName = "user", Departament = departaments[1], Firstname = "Patryk", Lastname = "Nowak"}
                };

                userManager.CreateAsync(user[0], "Boss123").Wait();
                userManager.AddClaimAsync(user[0], new Claim(ClaimTypes.Role, "boss")).Wait();
                userManager.AddClaimAsync(user[0], new Claim(ClaimTypes.Role, "admin")).Wait();

                userManager.CreateAsync(user[1], "Admin123").Wait();
                userManager.AddClaimAsync(user[1], new Claim(ClaimTypes.Role, "admin")).Wait();

                userManager.CreateAsync(user[2], "User123").Wait();
                userManager.AddClaimAsync(user[2], new Claim(ClaimTypes.Role, "user")).Wait();
            }
        }
    }
}
