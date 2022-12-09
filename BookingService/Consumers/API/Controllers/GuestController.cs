using Application.Enum;
using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/guest")]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IGuestManager _ports;
        public GuestController(ILogger<GuestController> logger, IGuestManager ports)
        {
            _logger = logger;
            _ports = ports;
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> Post(GuestDTO guestDTO)
        {
            var request = new CreateGuestRequest { Data = guestDTO };
            var res = await _ports.CreateGuest(request);
            if (res.Success) return Created("", res.Data);
            if (res.ErrorCode == ErrorCodes.NOT_FOUND)
            {
                return BadRequest(res);
            }
            _logger.LogError("Response with unknown Error code Returned", res);
            return BadRequest(500);
        }
    }
}
