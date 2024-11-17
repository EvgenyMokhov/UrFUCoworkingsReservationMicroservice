using UrFUCoworkingsReservationMicroservice.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Data.Interfaces
{
    public interface IVisits
    {
        public Task CreateVisitAsync(Visit visit);
        public Task<IEnumerable<Visit>> GetVisitsByReservationIdAsync(Guid reservationId);
        public Task DeleteVisitAsync(Visit visit);
    }
}
