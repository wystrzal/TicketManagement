using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.AccountDtos;
using TicketManagement.API.Core.Interfaces;
using TicketManagement.API.Core.Interfaces.DepartamentInterfaces;
using TicketManagement.API.Core.Models;

namespace TicketManagement.API.Infrastructure.Services
{
    public class DepartamentService : IDepartamentService
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartamentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddDepartament(CreateDepartamentDto createDepartament)
        {
            var departamentToCreate = unitOfWork.Mapper().Map<Departament>(createDepartament);

            unitOfWork.Repository<Departament>().Add(departamentToCreate);

            return await unitOfWork.SaveAllAsync();
        }

        public async Task<List<GetDepartamentDto>> GetDepartaments()
        {
            var departaments = await unitOfWork.Repository<Departament>().GetAll();

            return unitOfWork.Mapper().Map<List<GetDepartamentDto>>(departaments);
        }
    }
}
