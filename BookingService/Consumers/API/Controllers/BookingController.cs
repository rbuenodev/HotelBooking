using Application.Booking.Commands;
using Application.Booking.DTOs;
using Application.Booking.Ports;
using Application.Booking.Queries;
using Application.Booking.Request;
using Application.Enum;
using Application.Payment.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager _bookingManager;
        private readonly ILogger<BookingController> _logger;
        private readonly IMediator _mediator;
        public BookingController(IBookingManager bookingManager, ILogger<BookingController> logger, IMediator mediator)
        {

            _bookingManager = bookingManager;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BookingDTO>> Post(BookingDTO bookingDto)
        {

            var command = new CreateBookingCommand
            {
                CreateBookingRequest = new CreateBookingRequest { BookingDto = bookingDto }
            };
            var res = await _mediator.Send(command);

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

        [HttpPost]
        [Route("{bookingId}/Pay")]
        public async Task<ActionResult<PaymentResponse>> Pay(PaymentRequest paymentRequestDto, int bookingId)
        {
            paymentRequestDto.BookingId = bookingId;
            var res = await _bookingManager.PayForABooking(paymentRequestDto);

            if (res.Success) return Ok(res.Data);

            return BadRequest(res);
        }

        [HttpGet]
        public async Task<ActionResult<BookingDTO>> Get(int id)
        {
            var query = new GetBookingQuery
            {
                Id = id
            };

            var res = await _mediator.Send(query);

            if (res.Success) return Created("", res.Data);

            _logger.LogError("Could not process the request", res);
            return BadRequest(res);
        }
    }
}
