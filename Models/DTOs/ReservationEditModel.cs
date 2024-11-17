using System.ComponentModel.DataAnnotations;
using UrFUCoworkingsReservationMicroservice.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Models.DTOs
{
    public class ReservationEditModel
    {
        public Guid ReservationId { get; set; }
        public Guid ReservatorId { get; set; }
        public DateOnly ReservationDay { get; set; }
        public string ReservationBegin { get; set; }
        public string ReservationEnd { get; set; }
        public List<Guid> PlacesIds { get; set; } = new();
        public List<Guid> UserIds { get; set; } = new();
    }
}
