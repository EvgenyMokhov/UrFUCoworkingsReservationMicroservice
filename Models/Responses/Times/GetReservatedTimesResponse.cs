namespace UrFUCoworkingsMicroservice.Models.Responses.Times
{
    public class GetReservatedTimesResponse
    {
        public List<(TimeOnly reservationBegin, TimeOnly reservationEnd)> ResponseData { get; set; }
    }
}
