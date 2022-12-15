using Domain.Bookings.Enums;
using Entities = Domain.Entities;

namespace Application.Booking.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        public DateTime Start { get; set; } = DateTime.UtcNow;
        public DateTime End { get; set; } = DateTime.UtcNow;
        public int Room { get; set; }
        public int Guest { get; set; }

        public static Entities.Booking MapToEntity(BookingDTO bookingDTO)
        {
            return new Entities.Booking
            {
                Id = bookingDTO.Id,
                PlacedAt = bookingDTO.PlacedAt,
                Start = bookingDTO.Start,
                End = bookingDTO.End,
                Guest = new Entities.Guest { Id = bookingDTO.Guest },
                Room = new Entities.Room { Id = bookingDTO.Room },
            };
        }

        public static BookingDTO MapToDto(Entities.Booking booking)
        {
            return new BookingDTO
            {
                Id = booking.Id,
                PlacedAt = booking.PlacedAt,
                Start = booking.Start,
                End = booking.End,
                Room = booking.Room.Id,
                Guest = booking.Guest.Id
            };
        }
    }
}
