using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public virtual DbSet<Guest> Guests { get; set; }
    }
}