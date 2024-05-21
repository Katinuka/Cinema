using Cinema.DAL.Implemantations;
using Microsoft.AspNetCore.Mvc;
using Cinema.BLL.TicketPdfGenerator;
using Cinema.DAL.Models;
using System.Threading.Tasks;

namespace Cinema.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly TicketPdfGenerator _ticketPdfGenerator;

        public TicketController(UnitOfWork unitOfWork, TicketPdfGenerator ticketPdfGenerator)
        {
            _unitOfWork = unitOfWork;
            _ticketPdfGenerator = ticketPdfGenerator;
        }

        [HttpGet("{id}/ticket")]
        public async Task<IActionResult> GetTicketAsync(int id)
        {
            var seatReservation = await _unitOfWork.SeatResarvationRepository.GetByIDAsync(id,
                x => x.Reservation,
                x => x.Reservation.Session,
                x => x.Reservation.Session.CinemaRoom,
                x => x.Reservation.Session.Movie);

            if (seatReservation == null)
            {
                return NotFound();
            }

            var pdfBytes = _ticketPdfGenerator.GeneratePdf(seatReservation);

            return File(pdfBytes, "application/pdf", "ticket.pdf");
        }
    }
}
