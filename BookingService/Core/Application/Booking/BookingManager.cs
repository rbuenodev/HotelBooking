using Application.Booking.DTOs;
using Application.Booking.Ports;
using Application.Booking.Request;
using Application.Booking.Response;
using Application.Payment.Ports;
using Application.Payment.Responses;
using Domain.Bookings.Exceptions;
using Domain.Bookings.Ports;
using Domain.Guests.Ports;
using Domain.Rooms.Exceptions;
using Domain.Rooms.Ports;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;
        public BookingManager(IBookingRepository bookingRepository, IRoomRepository roomRepository, IGuestRepository guestRepository, IPaymentProcessorFactory paymentProcessorFactory)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _guestRepository = guestRepository;
            _paymentProcessorFactory = paymentProcessorFactory;
        }
        public async Task<BookingResponse> CreatBooking(CreateBookingRequest bookingRequest)
        {
            try
            {
                var booking = BookingDTO.MapToEntity(bookingRequest.BookingDto);
                booking.Guest = await _guestRepository.Get(bookingRequest.BookingDto.Guest);
                booking.Room = await _roomRepository.GetAggregate(bookingRequest.BookingDto.Room);
                await booking.Save(_bookingRepository);
                bookingRequest.BookingDto.Id = booking.Id;
                return new BookingResponse
                {
                    Success = true,
                    Data = bookingRequest.BookingDto,
                };
            }
            catch (RequiredDateException e)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = Enum.ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = e.Message,
                };
            }
            catch (RequiredGuestIsNullException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = Enum.ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "Guest is a required Information",
                };
            }
            catch (RequiredRoomIsNullExeption)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = Enum.ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "Room is a required Information",
                };

            }
            catch (RoomCantBeBookedException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = Enum.ErrorCodes.BOOKING_ROOM_CANNOT_BE_BOOKED,
                    Message = "Room can't be booked",
                };
            }
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = Enum.ErrorCodes.BOOKING_COULD_NOT_STORE_DATA,
                    Message = "There was an error when trying to save to DB",
                };
            }
        }

        public Task<BookingResponse> GetBooking(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaymentResponse> PayForABooking(PaymentRequest paymentRequestDto)
        {
            var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(paymentRequestDto.SelectedPaymentProvider);

            var response = await paymentProcessor.CapturePayment(paymentRequestDto.PaymentIntention);

            if (response.Success)
            {
                return new PaymentResponse
                {
                    Success = true,
                    Data = response.Data,
                    Message = "Payment successfully processed"
                };
            }

            return response;
        }
    }
}
