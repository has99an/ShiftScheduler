using Microsoft.AspNetCore.Mvc;
using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.BusinessLogicLayer;

namespace ShiftSchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeLogic _employeeLogic;

        public EmployeeController(EmployeeLogic employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeLogic.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeLogic.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            var insertedId = await _employeeLogic.AddEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = insertedId }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            await _employeeLogic.UpdateEmployee(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeLogic.DeleteEmployee(id);
            return NoContent();
        }
    }
}
