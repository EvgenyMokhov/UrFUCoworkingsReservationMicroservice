using UrFUCoworkingsReservationMicroservice.Models.DTOs;

namespace UrFUCoworkingsReservationMicroservice.Models.Responses.Reservations
{
    public class GetReservationsResponse
    {
        public List<ReservationViewModel> ResponseData { get; set; }
    }
}
