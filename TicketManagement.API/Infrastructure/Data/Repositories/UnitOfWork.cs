﻿using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.Core.Interfaces;

namespace TicketManagement.API.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        private Dictionary<Type, object> repositories;

        public UnitOfWork(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public IMapper Mapper()
        {
            return mapper;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(T);

            if (!repositories.ContainsKey(type))
            {
                var repo = Activator.CreateInstance(typeof(Repository<>).MakeGenericType(typeof(T)), dataContext);

                repositories.Add(type, repo);
            }

            return (IRepository<T>)repositories[type];
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dataContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
