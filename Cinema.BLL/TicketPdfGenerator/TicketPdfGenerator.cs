using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Cinema.DAL.Models;
using System.IO;
using PdfSharp.Fonts;
using System.Reflection;

namespace Cinema.BLL.TicketPdfGenerator
{
    public class TicketPdfGenerator
    {
        public byte[] GeneratePdf(SeatReservation seatReservation)
        {
            if (seatReservation.Reservation == null || seatReservation.Session == null || seatReservation.Session.Movie == null || seatReservation.Session.CinemaRoom == null)
            {
                throw new ArgumentException("SeatReservation object is missing required related data.");
            }

            var reservation = seatReservation.Reservation;
            var session = seatReservation.Session;
            var movie = session.Movie;
            var cinemaRoom = session.CinemaRoom;

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Cinema Ticket";

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 12, XFontStyleEx.Regular);
            XFont boldFont = new XFont("Verdana", 12, XFontStyleEx.Bold);

            gfx.DrawString("Квиток", boldFont, XBrushes.Black, new XRect(0, 20, page.Width, page.Height), XStringFormats.TopCenter);
            gfx.DrawString($"Назва фільму: {movie.Title}", font, XBrushes.Black, new XRect(40, 50, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Час початку: {TimeSpan.FromMinutes(session.SessionTime ?? 0):hh\\:mm}", font, XBrushes.Black, new XRect(40, 70, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Тривалість: {movie.DurationTime} хв", font, XBrushes.Black, new XRect(40, 90, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Зал: {cinemaRoom.Name}", font, XBrushes.Black, new XRect(40, 110, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Номер місця: {seatReservation.NumberOfSeat}", font, XBrushes.Black, new XRect(40, 130, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Ціна: {reservation.TotalSum:C}", font, XBrushes.Black, new XRect(40, 150, page.Width, page.Height), XStringFormats.TopLeft);

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                return stream.ToArray();
            }
        }
    }
}