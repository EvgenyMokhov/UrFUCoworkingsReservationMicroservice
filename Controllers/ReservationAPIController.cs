using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsReservationMicroservice.Business_Logic;
using UrFUCoworkingsReservationMicroservice.Models.DTOs;

namespace UrFUCoworkingsReservationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationAPIController : ControllerBase
    {
        private readonly ServiceManager serviceManager;
        public ReservationAPIController(IServiceProvider provider)
        {
            serviceManager = new(provider);
        }

        [HttpGet(Name = "GetReservations")]
        public async Task<ReservationViewModel> GetReservationsAsync([FromQuery] Guid userId)
        {
            return new();
        }

        [HttpPost(Name = "CreateReservation")]
        public async Task CreateReservationAsync([FromBody] ReservationEditModel reservation)
        {
            await serviceManager.ReservationService.CreateReservationAsync(reservation);
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
