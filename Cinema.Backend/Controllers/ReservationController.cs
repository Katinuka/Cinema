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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = $"{SD.Admin},{SD.Customer}")]
    public class ReservationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ReservationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetReservations")]
        public async Task<IEnumerable<Reservation>> GetReservationsAsync() => await _unitOfWork.ReservationRepository.Get();


        [HttpPost("AddReservation")]
        public async Task<IActionResult> AddReservationAsync([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _unitOfWork.ReservationRepository.InsertAsync(reservation);
            await _unitOfWork.SaveAsync();
            return Ok();
        }


        [HttpDelete("DeleteReservation/{id}")]
        public async Task<IActionResult> DeleteReservationAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIDAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            await _unitOfWork.ReservationRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        [HttpPut("UpdateReservation/{id}")]
        public async Task<IActionResult> UpdateReservationAsync(int id, [FromBody] Reservation updatedReservation)
        {
            if (id != updatedReservation.ReservationId)
            {
                return BadRequest();
            }

            try
            {
                await _unitOfWork.ReservationRepository.UpdateAsync(id, updatedReservation);
                await _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _unitOfWork.ReservationRepository.GetByIDAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
        }

    }
}