using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsReservationMicroservice.Models.DTOs;

namespace UrFUCoworkingsReservationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationAPIController : ControllerBase
    {
        [HttpGet(Name = "GetReservations")]
        public async Task<ReservationViewModel> GetReservationsAsync([FromQuery] int userId)
        {
            return new();
        }

        [HttpPost(Name = "CreateReservation")]
        public async Task CreateReservationAsync([FromBody] ReservationEditModel reservation)
        {
            
        }

        [HttpPut(Name = "UpdateReservation")]
        public async Task UpdateReservationAsync([FromBody] ReservationEditModel reservation)
        {
            
        }

        [HttpDelete(Name = "DeleteReservation")]
        public async Task DeleteReservationAsync([FromQuery] int reservationId)
        {
            
        }
    }
}
