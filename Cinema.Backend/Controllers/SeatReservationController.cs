using Cinema.DAL;
using Cinema.DAL.Implemantations; 
using Cinema.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = $"{SD.Admin}")]
    public class SeatReservationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public SeatReservationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<List<SeatReservation>>> GetAllAsync() => Ok(await _unitOfWork.SeatResarvationRepository
            .Get(includeProperties:
                "Reservation," +
                "Reservation.Session," +
                "Reservation.Session.CinemaRoom," +
                "Reservation.Session.Movie," +
                "Reservation.Session.Movie.Genre," +
                "Reservation.ApplicationUser," +
                "Session," +
                "Session.CinemaRoom," +
                "Session.Movie," +
                "Session.Movie.Genre")); 
        
        [HttpGet("{id}")] 
        public async Task<ActionResult<SeatReservation>> GetByIdAsync(int id)
        {
            var seatReservation = await _unitOfWork.SeatResarvationRepository.GetByIDAsync(id);
            if (seatReservation == null)
            {
                return NotFound();
            }
            return Ok(seatReservation);
        }

        [HttpPost]
        public async Task<ActionResult<SeatReservation>> CreateAsync(SeatReservation model)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.SeatResarvationRepository.InsertAsync(model);
            await _unitOfWork.SaveAsync();

            return CreatedAtRoute("GetSeatReservation", new { id = model.Id }, model); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, SeatReservation model)
        {
            if (id != model.Id) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }


            await _unitOfWork.SeatResarvationRepository.UpdateAsync(id, model);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var seatReservation = await _unitOfWork.SeatResarvationRepository.GetByIDAsync(id);
            if (seatReservation == null)
            {
                return NotFound();
            }

            await _unitOfWork.SeatResarvationRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
    }
}
