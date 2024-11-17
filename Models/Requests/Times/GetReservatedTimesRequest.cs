namespace UrFUCoworkingsReservationMicroservice.Models.Requests.Times
{
    public class GetReservatedTimesRequest
    {
        public Guid PlaceId { get; init; }
        public DateOnly Date { get; init; }
    }
}
