using UrFUCoworkingsReservationMicroservice.Data;
using UrFUCoworkingsReservationMicroservice.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Business_Logic.Services
{
    public class VisitService
    {
        private readonly IServiceProvider serviceProvider;
        public VisitService(IServiceProvider provider) => serviceProvider = provider;
        public async Task<Visit> CreateVisitAsync(Guid userId, Guid reservationId)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            Visit visit = new();
            visit.UserId = userId;
            visit.ReservationId = reservationId;
            visit.Reservation = await dataManager.Reservations.GetReservationAsync(reservationId);
            visit.User = await dataManager.Users.GetUserAsync(userId);
            visit.Id = Guid.NewGuid();
            await dataManager.Visits.CreateVisitAsync(visit);
            return visit;
        }

        public async Task DeleteVisitAsync(Visit visit)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            await dataManager.Visits.DeleteVisitAsync(visit);
        }

        public async Task<List<Visit>> GetVisitsAsync(Guid reservationId)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            return (await dataManager.Visits.GetVisitsByReservationIdAsync(reservationId)).ToList();
        }
    }
}
