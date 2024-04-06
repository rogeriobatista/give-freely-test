using EmployeeManagementSystem.Domain.Employees.Dtos;
using EmployeeManagementSystem.Domain.Employees.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Api.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string? filter = null)
        {
            var response = await _service.GetAsync(filter);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var response = await _service.GetAsync(id);

            if (response != null)
                return Ok(response);

            return NotFound();
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EmployeeDto dto)
        {
            try
            {
                var response = await _service.CreateAsync(dto);

                if (response != null && !response.Errors.Any())
                    return Created("", response);

                return BadRequest(response?.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] EmployeeDto dto)
        {
            var response = await _service.UpdateAsync(id, dto);

            if (response != null && !response.Errors.Any())
                return NoContent();

            return BadRequest(response?.Errors);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.DeleteAsync(id);
        }
   
    }
}