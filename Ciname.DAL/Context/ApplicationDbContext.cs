


using Cinema.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<CinemaRoom> CinemaRooms { get; set; }

        public DbSet<Reservation> Reservations { get; set; }


        public DbSet<SeatReservation> SeatReservations { get; set; }

        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
