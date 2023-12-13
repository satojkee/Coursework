using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;

namespace Hotel.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApartmentModel> Apartments {  get; set; }
        public DbSet<ApartmentTypeModel> ApartmentTypes { get; set; }
        public DbSet<FloorModel> Floors { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }
        public DbSet<ApartmentImageModel> Images { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // One RoomType many Rooms
            builder.Entity<ApartmentModel>()
                .HasOne(t => t.Type)
                .WithMany(t => t.Apartments)
                .HasForeignKey(t => t.TypeId);

            // One Floor many Rooms
            builder.Entity<ApartmentModel>()
                .HasOne(t => t.Floor)
                .WithMany(t => t.Apartments)
                .HasForeignKey(t => t.FloorId);

            // One Room many Reservations
            builder.Entity<ReservationModel>()
                .HasOne(t => t.Apartment)
                .WithMany(t => t.Reservations)
                .HasForeignKey(t => t.ApartmentId);

            // One Room many Images
            builder.Entity<ApartmentImageModel>()
                .HasOne(t => t.Apartment)
                .WithMany(t => t.Images)
                .HasForeignKey(t => t.ApartmentId);
        }
    }
}