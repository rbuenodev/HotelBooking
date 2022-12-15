using Application.Booking.DTOs;
using Application.Booking.Response;
using Domain.Bookings.Ports;
using MediatR;

namespace Application.Booking.Queries
{
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingResponse>
    {

        private IBookingRepository _bookingRepository;
        public GetBookingQueryHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingResponse> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetAggregate(request.Id);
            if (booking == null)
            {
                return new BookingResponse
                {
                    Success = false,
                    Message = "Booking not found",
                    ErrorCode = Enum.ErrorCodes.BOOKING_NOT_FOUND
                };
            }

            var bookingDto = BookingDTO.MapToDto(booking);
            return new BookingResponse
            {
                Success = true,
                Data = bookingDto,
            };
        }
    }
}
