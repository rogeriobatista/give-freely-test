using EmployeeManagementSystem.Domain.Users.Dtos;
using EmployeeManagementSystem.Domain.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var response = await _service.GetAsync();

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

        [AllowAnonymous]
        [HttpPost("sign-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SignIn([FromBody] UserSignInDto dto)
        {
            var response = await _service.SignInAsync(dto);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp([FromBody] UserDto dto)
        {
            try
            {
                var response = await _service.CreateAsync(dto);

                if (response != null && !response.Errors.Any())
                    return Created("sign-up", response);

                return BadRequest(response?.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromRoute] int id, [FromBody] string password)
        {
            var response = await _service.ChangePasswordAsync(id, password);

            if (response != null && !response.Errors.Any())
                return NoContent();

            return BadRequest(response?.Errors);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UserDto dto)
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