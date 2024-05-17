


using Cinema.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.DAL.Context
{
    public class ApplicationDbContext : DbContext
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasOne(it => it.Genre)
                .WithMany()
                .HasForeignKey(it => it.GenreId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Session>()
                .HasOne(it => it.Movie)
                .WithMany()
                .HasForeignKey(it => it.MovieId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Session>()
                .HasOne(it => it.CinemaRoom)
                .WithMany()
                .HasForeignKey(it => it.CinemaRoomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Reservation>()
                .HasOne(it => it.Session)
                .WithMany()
                .HasForeignKey(it => it.SessionId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Reservation>()
                .HasOne(it => it.ApplicationUser)
                .WithMany()
                .HasForeignKey(it => it.ApplicationUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SeatReservation>()
                .HasOne(it => it.Reservation)
                .WithMany()
                .HasForeignKey(it => it.ReservationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<SeatReservation>()
                .HasOne(it => it.Session)
                .WithMany()
                .HasForeignKey(it => it.SessionId)
                .OnDelete(DeleteBehavior.SetNull);


            //Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Comedy" },
                new Genre { Id = 3, Name = "Drama" },
                new Genre { Id = 4, Name = "Horror" },
                new Genre { Id = 5, Name = "Sci-Fi" }
            );


            //Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = 1,
                    Title = "Inception",
                    Director = "Christopher Nolan",
                    Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page",
                    GenreId = 5,
                    Description = "A thief who steals corporate secrets through the use of dream-sharing technology.",
                    DurationTime = 148,
                    ReleaseDate = new DateTimeOffset(2010, 7, 16, 0, 0, 0, TimeSpan.Zero),
                    Price = 9.99m,
                    NowShowing = true
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Shawshank Redemption",
                    Director = "Frank Darabont",
                    Cast = "Tim Robbins, Morgan Freeman",
                    GenreId = 3,
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    DurationTime = 142,
                    ReleaseDate = new DateTimeOffset(1994, 10, 14, 0, 0, 0, TimeSpan.Zero),
                    Price = 8.99m,
                    NowShowing = true
                },
                new Movie
                {
                    Id = 3,
                    Title = "Pulp Fiction",
                    Director = "Quentin Tarantino",
                    Cast = "John Travolta, Uma Thurman, Samuel L. Jackson",
                    GenreId = 2,
                    Description = "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    DurationTime = 154,
                    ReleaseDate = new DateTimeOffset(1994, 10, 14, 0, 0, 0, TimeSpan.Zero),
                    Price = 7.99m,
                    NowShowing = true
                },
                new Movie
                {
                    Id = 4,
                    Title = "The Exorcist",
                    Director = "William Friedkin",
                    Cast = "Ellen Burstyn, Max von Sydow, Linda Blair",
                    GenreId = 4,
                    Description = "When a teenage girl is possessed by a mysterious entity, her mother seeks the help of two priests to save her daughter.",
                    DurationTime = 122,
                    ReleaseDate = new DateTimeOffset(1973, 12, 26, 0, 0, 0, TimeSpan.Zero),
                    Price = 6.99m,
                    NowShowing = true
                }
            );

            //Rooms
            modelBuilder.Entity<CinemaRoom>().HasData(
                new CinemaRoom
                {
                    Id = 1,
                    Name = "Room 1",
                    Rows = 10,
                    AmountOfSeats = 15
                },
                new CinemaRoom
                {
                    Id = 2,
                    Name = "Room 2",
                    Rows = 8,
                    AmountOfSeats = 12
                },
                new CinemaRoom
                {
                    Id = 3,
                    Name = "Room 3",
                    Rows = 12,
                    AmountOfSeats = 20
                }
            );


            //Sessions
            modelBuilder.Entity<Session>().HasData(
                new Session
                {
                    SessionId = 1,
                    MovieId = 1,
                    CinemaRoomId = 1,
                    SessionTime = 10.00
                },
                new Session
                {
                    SessionId = 2,
                    MovieId = 2,
                    CinemaRoomId = 2,
                    SessionTime = 12.30
                },
                new Session
                {
                    SessionId = 3,
                    MovieId = 3,
                    CinemaRoomId = 3,
                    SessionTime = 15.00
                },
                new Session
                {
                    SessionId = 4,
                    MovieId = 4,
                    CinemaRoomId = 1,
                    SessionTime = 18.00
                }
            );


            //Users
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1,
                    LastName = "Doe",
                    FirstName = "John",
                    Email = "john.doe@example.com",
                    Password = "$2a$11$r5Ibl/l0kOGDZv/ihgepie1G0hcTA8Bb51RaTSNptysM6EXzkXr6u",  // password123
                    PhoneNumber = "1234567890",
                    Role = "User"
                },
                new ApplicationUser
                {
                    Id = 2,
                    LastName = "Smith",
                    FirstName = "Jane",
                    Email = "jane.smith@example.com",
                    Password = "$2a$11$nSB6o2EjgF5tvD3q76/up.oHvFIDklRmUK/gAFsGQIjMFiqSWimR.",  // password456
                    PhoneNumber = "0987654321",
                    Role = "Admin"
                },
                new ApplicationUser
                {
                    Id = 3,
                    LastName = "Johnson",
                    FirstName = "Michael",
                    Email = "michael.johnson@example.com",
                    Password = "$2a$11$5ou33R/J500lRf3XGjynGu6woXpX0uoHALgDHTu3Bt8ybM1WPFVW.",  // password789
                    PhoneNumber = "5551234567",
                    Role = "User"
                },
                new ApplicationUser
                {
                    Id = 4,
                    LastName = "Williams",
                    FirstName = "Emily",
                    Email = "emily.williams@example.com",
                    Password = "$2a$11$Y9xnlVeuTYN0WsYYLzLgVuDb0mPXgqRpecoJq5btn.XHGD4Gz50SG",  // passwordABC
                    PhoneNumber = "9876543210",
                    Role = "User"
                }
            );


            //Reservations
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    ReservationId = 1,
                    SessionId = 1,
                    ApplicationUserId = 1,
                    TotalSum = 25.00m,
                    Reserved = true,
                    IsPaid = true,
                    IsActive = true
                },
                new Reservation
                {
                    ReservationId = 2,
                    SessionId = 2,
                    ApplicationUserId = 2,
                    TotalSum = 30.00m,
                    Reserved = true,
                    IsPaid = false,
                    IsActive = true
                },
                new Reservation
                {
                    ReservationId = 3,
                    SessionId = 3,
                    ApplicationUserId = 3,
                    TotalSum = 18.00m,
                    Reserved = true,
                    IsPaid = true,
                    IsActive = false
                }
            );


            //SeatReservations
            modelBuilder.Entity<SeatReservation>().HasData(
                new SeatReservation
                {
                    Id = 1,
                    NumberOfSeat = 1,
                    ReservationId = 1,
                    SessionId = 1
                },
                new SeatReservation
                {
                    Id = 2,
                    NumberOfSeat = 2,
                    ReservationId = 2,
                    SessionId = 1
                },
                new SeatReservation
                {
                    Id = 3,
                    NumberOfSeat = 3,
                    ReservationId = 3,
                    SessionId = 2
                }
            );



        }

    }
}
