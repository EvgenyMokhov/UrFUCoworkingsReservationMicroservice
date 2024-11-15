namespace UrFUCoworkingsMicroservice.Data.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid ReservatorId { get; set; }
        public DateTime ReservationBegin { get; set; }
        public DateTime ReservationEnd { get; set; }
        public virtual List<Visit> Visits { get; set; } = new();
        public virtual List<Place> Places { get; set; } = new();
    }
}
