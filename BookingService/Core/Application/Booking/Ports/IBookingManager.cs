using Application.Booking.Request;
using Application.Booking.Response;
using Application.Payment.Responses;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreatBooking(CreateBookingRequest bookingDto);
        Task<BookingResponse> GetBooking(int id);
        Task<PaymentResponse> PayForABooking(PaymentRequest paymentRequestDto);
    }
}
