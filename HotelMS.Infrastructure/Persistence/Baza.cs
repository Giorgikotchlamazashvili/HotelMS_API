using HotelMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HotelMS.Infrastructure.Persistence
{
    public class Baza : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Bookings> Bookings { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Payment> Payments { get; set; }

        private readonly IConfiguration config;


        public Baza(IConfiguration _config)
        {
            config = _config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentMethod>().HasData(
                new PaymentMethod { Id = 1, Name = "Credit Card" },
                new PaymentMethod { Id = 2, Name = "Cash" },
                new PaymentMethod { Id = 3, Name = "Bank Transfer" }
            );
        }
    }
}
