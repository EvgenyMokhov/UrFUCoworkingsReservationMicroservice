using Microsoft.EntityFrameworkCore;
using UrFUCoworkingsReservationMicroservice.Data.Entities;
using System.Collections.Generic;

namespace UrFUCoworkingsReservationMicroservice.Data
{
    public class EFDBContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Visit> Visits { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Place> Places { get; set; } = null!;
        public EFDBContext(DbContextOptions<EFDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
        }
    }
}
