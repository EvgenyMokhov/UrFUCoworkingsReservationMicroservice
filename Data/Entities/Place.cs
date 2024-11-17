namespace UrFUCoworkingsReservationMicroservice.Data.Entities
{
    public class Place
    {
        public Guid Id { get; set; }
        public virtual List<Reservation> Reservations { get; set; } = new();
        public virtual Zone Zone { get; set; } = null!;
    }
}
