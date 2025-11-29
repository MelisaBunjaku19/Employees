using EmployeesApi.Models;
using EmployeesApi.Services;
using EmployeesApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

namespace EmployeesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDto>>> GetAllEmployees()
        {
            var employees = await _service.GetAllAsync();
            var dtos = employees.Select(e => MapToGetDto(e)).ToList();
            return Ok(dtos); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDto>> GetEmployee(Guid id)
        {
            var employee = await _service.GetByIdAsync(id);
            if (employee == null) return NotFound("Employee not found.");

            return Ok(MapToGetDto(employee)); 
        }

        [HttpPost]
        public async Task<ActionResult<GetEmployeeDto>> PostEmployee(CreateEmployeeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName))
            {
                return BadRequest("FirstName and LastName are required.");
            }

            var employee = new Employee
            {
                FirstName = dto.FirstName ?? string.Empty,
                LastName = dto.LastName ?? string.Empty,
                DateOfBirth = dto.DateOfBirth,
                EducationLevel = dto.EducationLevel ?? string.Empty
            };

            var created = await _service.CreateAsync(employee);

            return CreatedAtAction(nameof(GetEmployee),
                new { id = created.Id },
                MapToGetDto(created)); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, UpdateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Id = id,
                FirstName = dto.FirstName ?? string.Empty,
                LastName = dto.LastName ?? string.Empty,
                DateOfBirth = dto.DateOfBirth,
                EducationLevel = dto.EducationLevel ?? string.Empty
            };

            var updated = await _service.UpdateAsync(employee);

            if (updated == null)
                return NotFound("Employee not found.");

            return Ok(new { message = "Employee updated successfully", employee = MapToGetDto(updated) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound("Employee not found.");

            return Ok(new { message = "Employee deleted successfully" });
        }

        private GetEmployeeDto MapToGetDto(Employee employee)
        {
            return new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                EducationLevel = employee.EducationLevel
            };
        }
    }
}