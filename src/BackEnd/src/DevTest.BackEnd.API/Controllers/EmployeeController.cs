using AutoMapper;
using Azure;
using DevTest.BackEnd.Data.Models;
using DevTest.BackEnd.Data.Repositories;
using DevTest.BackEnd.Service;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevTest.Shared.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

// For 

namespace DevTest.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeController( IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;   
            _mapper = mapper;
        }

        [HttpPost]
        [Route("employeesFilter")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetFilteredEmployees(FilterDTO filter)
        {
            try
            {
                var employees = await _employeeService.Get();

                if (!string.IsNullOrEmpty(filter.RFC))
                {
                    employees = employees.Where(e => e.RFC.Contains(filter.RFC)).ToList();
                }

                if (filter.BornDate != null && filter.BornDate != DateTime.MinValue)
                {
                    employees = employees.Where(e => e.BornDate.Date == filter.BornDate).ToList();
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Get()
        {
            var employees = await _employeeService.Get();
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Post(EmployeeDTO employee)
        {
            if (await _employeeService.IsNewRFC(employee.RFC))
            {
                var createdEmployee = await _employeeService.Create(_mapper.Map<Employee>(employee));
                return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.ID }, createdEmployee);
            }
            
            return BadRequest("Ya existe un empleado con ese RFC");
            
        }

        [HttpGet]
        [Route("employeesRFC")]
        public async Task<ActionResult<Employee>> GetByRFC(string rfc)
        {
            var employees = await _employeeService.GetByRFC(rfc);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);

        }

        [HttpGet]
        [Route("employeesBornDate")]
        public async Task<ActionResult<Employee>> GetByBornDate(DateTime bornDate)
        {
            var employees = await _employeeService.GetByBornDate(bornDate);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, EmployeeDTO employee)
        {
            var updatedEmployee = await _employeeService.Update(id, _mapper.Map<Employee>(employee));
            if (updatedEmployee == null)
            {
                return NotFound();
            }

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await _employeeService.Get(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.Delete(id);
            return NoContent();
        }
    }
}
