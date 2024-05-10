using Cinema.DAL.Models;
using Cinema.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.DAL.Implemantations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = SD.Admin)]
    public class GenreController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public GenreController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetGenres")]
        
        public async Task<IEnumerable<Genre>> GetGenresAsync() => await _unitOfWork.GenreRepository.Get();

        [HttpPost("AddGenre")]
        public async Task<IActionResult> AddGenreAsync([FromBody] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.GenreRepository.InsertAsync(genre);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete("DeleteGenre/{id}")]
        public async Task<IActionResult> DeleteGenreAsync(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetByIDAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            await _unitOfWork.GenreRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPut("UpdateGenre/{id}")]
        public async Task<IActionResult> UpdateGenreAsync(int id, [FromBody] Genre updatedGenre)
        {
            if (id != updatedGenre.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.GenreRepository.UpdateAsync(id, updatedGenre);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
