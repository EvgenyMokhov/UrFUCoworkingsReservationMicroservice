using UrFUCoworkingsModels.Data.Entities;
using UrFUCoworkingsReservationMicroservice.Data;

namespace UrFUCoworkingsReservationMicroservice.Business_Logic.Services
{
    public class VisitService
    {
        private readonly IServiceProvider serviceProvider;
        public VisitService(IServiceProvider provider) => serviceProvider = provider;
        public async Task<Visit> CreateVisitAsync(Guid userId, Guid reservationId)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
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
            DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
            await dataManager.Visits.DeleteVisitAsync(visit);
        }
    }
}
