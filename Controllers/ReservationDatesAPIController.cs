using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsReservationMicroservice.Data.Entities;
using UrFUCoworkingsReservationMicroservice.Models.DTOs;

namespace UrFUCoworkingsReservationMicroservice.Controllers
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
