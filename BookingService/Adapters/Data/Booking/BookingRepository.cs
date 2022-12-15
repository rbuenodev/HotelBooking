using Domain.Bookings.Ports;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<Entities.Booking> CreateBooking(Entities.Booking booking)
        {
            _hotelDbContext.Bookings.Add(booking);
            await _hotelDbContext.SaveChangesAsync();
            return booking;
        }

        public Task<Entities.Booking?> Get(int id)
        {
            return _hotelDbContext.Bookings.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<Entities.Booking> GetAggregate(int Id)
        {
            return _hotelDbContext.Bookings
                .Include(r => r.Room)
                .Include(g => g.Guest)
                .Where(x => x.Id == Id).FirstAsync();
        }
    }
}
