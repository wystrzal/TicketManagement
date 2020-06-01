using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Infrastructure.Data.Repositories;
using TicketManagement.API.Infrastructure.Services;
using TicketManagement.API.Infrastructure.Services.SearchIssueStrategy;

namespace TicketManagement.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<ISearchIssuesBox, SearchIssuesBox>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IIssueRepository, IssueRepository>();
        }
    }
}
