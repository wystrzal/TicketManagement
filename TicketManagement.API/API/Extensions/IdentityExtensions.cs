using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.API.Core.Models;
using TicketManagement.API.Infrastructure.Data;

namespace TicketManagement.API.Extensions
{
    public static class IdentityExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<User>>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                      .GetBytes(configuration.GetSection("AppSettings:Token").Value)),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Boss", policy => policy.RequireClaim(ClaimTypes.Role, "boss"));
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "admin", "boss"));
                options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "user", "boss", "admin"));
            });
        }
    }
}
