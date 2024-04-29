using Cinema.DAL;
using Cinema.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UnitOfWork _unitOfWork;

        public UserController(ILogger<UserController> logger,
            UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetUsers")]
        public async Task<List<User>> GetUsersAsync() => await _unitOfWork.UserRepository.Get().ToListAsync();

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIDAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _unitOfWork.UserRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }

            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(updatedUser);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.UserRepository.GetByIDAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
        }
    }
}
