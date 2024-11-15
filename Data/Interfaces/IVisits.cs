using UrFUCoworkingsMicroservice.Data.Entities;

namespace UrFUCoworkingsMicroservice.Data.Interfaces
{
    public interface IVisits
    {
        public Task<IEnumerable<Visit>> GetAllVisitsAsync();
        public Task<Visit> GetVisitAsync(Guid guid);
        public Task<Visit> GetVisitAsync(Guid userId, Guid reservationId);
        public Task UpdateVisitAsync(Visit visit);
        public Task CreateVisitAsync(Visit visit);
        public Task<IEnumerable<Visit>> GetVisitsByReservationIdAsync(Guid reservationId);
    }
}
