using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.API.API.Dtos.AccountDtos;

namespace TicketManagement.API.Core.Interfaces.DepartamentInterfaces
{
    public interface IDepartamentService
    {
        Task<bool> AddDepartament(CreateDepartamentDto createDepartament);
        Task<List<GetDepartamentDto>> GetDepartaments();
        Task<bool> DeleteDepartament(int departamentId);
    }
}
