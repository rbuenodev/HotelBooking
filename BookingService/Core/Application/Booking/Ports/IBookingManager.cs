using Application.Booking.DTOs;
using Application.Booking.Request;
using Application.Booking.Response;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreatBooking(CreateBookingRequest bookingDto);
        Task<BookingResponse> GetBooking(int id);
    }
}
