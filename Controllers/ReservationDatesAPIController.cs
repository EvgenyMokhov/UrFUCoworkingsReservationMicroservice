using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsMicroservice.Models.DTOs;

namespace UrFUCoworkingsMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationTimesController : ControllerBase
    {
        [HttpGet(Name = "GetReservatedTimes")]
        public async Task<List<TimeOnly>> GetReservatedTimesAsync([FromBody] PlaceView place, [FromQuery] DateOnly date)
        {
            return new();
        }
    }
}
