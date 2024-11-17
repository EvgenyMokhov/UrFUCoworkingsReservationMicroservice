using UrFUCoworkingsReservationMicroservice.Data;
using UrFUCoworkingsReservationMicroservice.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Business_Logic.Services
{
    public class DeleteDataBackgroundService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public DeleteDataBackgroundService(IServiceProvider provider) => serviceProvider = provider;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            List<Reservation> allReservations = new();
            while (!stoppingToken.IsCancellationRequested)
            {
                allReservations = await dataManager.Reservations.GetAllReservationsAsync();
                foreach (Reservation reservation in allReservations)
                {
                    if (TimeOnly.FromDateTime(reservation.ReservationEnd).AddMinutes(5) <= TimeOnly.FromDateTime(DateTime.Now))
                    {
                        await dataManager.Reservations.DeleteReservationAsync(reservation);
                    }
                }
                
            }
        }
    }
}
