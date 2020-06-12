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
        private readonly IMapper mapper;

        public DepartamentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> AddDepartament(CreateDepartamentDto createDepartament)
        {
            var departamentToCreate = mapper.Map<Departament>(createDepartament);

            unitOfWork.Repository<Departament>().Add(departamentToCreate);

            return await unitOfWork.SaveAllAsync();
        }

        public async Task<List<GetDepartamentDto>> GetDepartaments()
        {
            var departaments = await unitOfWork.Repository<Departament>().GetAll();

            return mapper.Map<List<GetDepartamentDto>>(departaments);
        }
    }
}
