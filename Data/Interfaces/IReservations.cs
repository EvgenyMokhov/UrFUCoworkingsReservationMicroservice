using UrFUCoworkingsModels.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Data.Interfaces
{
    public interface IReservations
    {
        public Task<List<Reservation>> GetUserReservationsAsync(Guid userId);
        public Task<List<Reservation>> GetAllReservationsAsync();
        public Task<Reservation> GetReservationAsync(Guid id);
        public Task UpdateReservationAsync(Reservation reservation);
        public Task CreateReservationAsync(Reservation resrvation);
        public Task DeleteReservationAsync(Reservation reservation);
        public Task<IEnumerable<Reservation>> GetReservationsOnDateAsync(Guid placeId, DateOnly date);
    }
}
