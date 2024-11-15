using UrFUCoworkingsMicroservice.Data.Entities;

namespace UrFUCoworkingsMicroservice.Data.Interfaces
{
    public interface IReservations
    {
        public Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        public Task<Reservation> GetReservationAsync(Guid id);
        public Task UpdateReservationAsync(Reservation reservation);
        public Task CreateReservationAsync(Reservation resrvation);
        public Task DeleteReservationAsync(Guid id);
        public Task<IEnumerable<Reservation>> GetReservationsOnDateAsync(Guid placeId, DateOnly date);
    }
}
