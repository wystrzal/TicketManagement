﻿using Microsoft.Extensions.DependencyInjection;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Interfaces.DepartamentInterfaces;
using TicketManagement.API.Core.Interfaces.MessageInterfaces;
using TicketManagement.API.Infrastructure.Data.Repositories;
using TicketManagement.API.Infrastructure.Services;
using TicketManagement.API.Infrastructure.Services.SearchIssue;

namespace TicketManagement.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IDepartamentService, DepartamentService>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<ISearchIssuesBox, SearchIssuesBox>();

            services.AddScoped<IMessageService, MessageService>();
        }
    }
}