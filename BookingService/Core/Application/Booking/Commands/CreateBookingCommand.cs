using Application.Booking.Request;
using Application.Booking.Response;
using MediatR;

namespace Application.Booking.Commands
{
    public class CreateBookingCommand : IRequest<BookingResponse>
    {
        public CreateBookingRequest CreateBookingRequest { get; set; }
    }
}
