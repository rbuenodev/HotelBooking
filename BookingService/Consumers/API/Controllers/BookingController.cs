using Application.Booking.DTOs;
using Application.Booking.Ports;
using Application.Booking.Request;
using Application.Enum;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _bookingManager;
        private readonly ILogger<BookingController> _logger;
        public BookingController(IBookingManager bookingManager, ILogger<BookingController> logger)
        {

            _bookingManager = bookingManager;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDTO>> Post(BookingDTO bookingDto)
        {
            var res = await _bookingManager.CreatBooking(new CreateBookingRequest { BookingDto = bookingDto });

            if (res.Success) return Created("", res.Data);
            switch (res.ErrorCode)
            {
                case ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION:
                    return BadRequest(res);
                case ErrorCodes.BOOKING_COULD_NOT_STORE_DATA:
                    return BadRequest(res);
                case ErrorCodes.NOT_FOUND:
                    return BadRequest(res);
                case ErrorCodes.COULD_NOT_STORE_DATA:
                    return BadRequest(res);
                case ErrorCodes.INVALID_PERSON_ID:
                    return BadRequest(res);
                case ErrorCodes.MISSING_REQUIRED_INFORMATION:
                    return BadRequest(res);
                case ErrorCodes.INVALID_EMAIL:
                    return BadRequest(res);
                default:
                    break;
            }

            _logger.LogError("Response with unkown error", res);
            return BadRequest(500);

        }
    }
}
