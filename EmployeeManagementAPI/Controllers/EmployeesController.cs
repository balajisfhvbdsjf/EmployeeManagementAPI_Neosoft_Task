using Microsoft.AspNetCore.Mvc;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Dtos;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        // GET: api/employees
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetAll()
        {
            try
            {
                var employees = _service.GetAllEmployees()
                    .Select(e => new EmployeeReadDto
                    {
                        Id = e.Id,
                        FullName = e.FullName,
                        Department = e.Department,
                        Salary = e.Salary
                    }).ToList();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching employees.", error = ex.Message });
            }
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public ActionResult<EmployeeReadDto> GetById(int id)
        {
            try
            {
                var employee = _service.GetEmployeeById(id);
                if (employee == null)
                    return NotFound(new { message = "Employee not found." });

                var dto = new EmployeeReadDto
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Department = employee.Department,
                    Salary = employee.Salary
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while fetching employee ID {id}.", error = ex.Message });
            }
        }

        // POST: api/employees
        [HttpPost]
        public IActionResult Create(EmployeeCreateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.FullName) ||
                    string.IsNullOrWhiteSpace(dto.Department) ||
                    dto.Salary <= 0)
                {
                    return BadRequest(new { message = "Invalid input." });
                }

                var employee = new Employee
                {
                    FullName = dto.FullName,
                    Department = dto.Department,
                    Salary = dto.Salary
                };

                var created = _service.CreateEmployee(employee);

                var readDto = new EmployeeReadDto
                {
                    Id = created.Id,
                    FullName = created.FullName,
                    Department = created.Department,
                    Salary = created.Salary
                };

                return CreatedAtAction(nameof(GetById), new { id = readDto.Id }, new
                {
                    message = "Employee created successfully.",
                    data = readDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the employee.", error = ex.Message });
            }
        }

        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, EmployeeUpdateDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.FullName) ||
                    string.IsNullOrWhiteSpace(dto.Department) ||
                    dto.Salary <= 0)
                {
                    return BadRequest(new { message = "Invalid input." });
                }

                var updated = new Employee
                {
                    FullName = dto.FullName,
                    Department = dto.Department,
                    Salary = dto.Salary
                };

                var result = _service.UpdateEmployee(id, updated);
                if (!result)
                    return NotFound(new { message = "Employee not found." });

                return Ok(new { message = "Employee updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating employee ID {id}.", error = ex.Message });
            }
        }

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _service.DeleteEmployee(id);
                if (!result)
                    return NotFound(new { message = "Employee not found." });

                return Ok(new { message = "Employee deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting employee ID {id}.", error = ex.Message });
            }
        }
    }
}
