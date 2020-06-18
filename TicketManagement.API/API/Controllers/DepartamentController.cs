using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.API.API.Dtos.AccountDtos;
using TicketManagement.API.Core.Interfaces.DepartamentInterfaces;

namespace TicketManagement.API.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentController : ControllerBase
    {
        private readonly IDepartamentService departamentService;

        public DepartamentController(IDepartamentService departamentService)
        {
            this.departamentService = departamentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartament(CreateDepartamentDto createDepartament)
        {
            if (ModelState.IsValid)
            {
                if (await departamentService.AddDepartament(createDepartament))
                {
                    return Ok();
                }

                return BadRequest("Something goes wrong.");
            }

            return BadRequest("Model state is not valid.");         
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartaments()
        {
            return Ok(await departamentService.GetDepartaments());
        }
    }
}