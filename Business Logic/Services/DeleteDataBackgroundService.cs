using UrFUCoworkingsModels.Data.Entities;
using UrFUCoworkingsReservationMicroservice.Data;

namespace UrFUCoworkingsReservationMicroservice.Business_Logic.Services
{
	public class DeleteDataBackgroundService : BackgroundService
	{
		private readonly IServiceProvider serviceProvider;
		public DeleteDataBackgroundService(IServiceProvider provider) => serviceProvider = provider;
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			using IServiceScope scope = serviceProvider.CreateScope();
			List<Reservation> allReservations = new();
			while (!stoppingToken.IsCancellationRequested)
			{
				DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
				allReservations = await dataManager.Reservations.GetAllReservationsAsync();
				foreach (Reservation reservation in allReservations)
				{
					if (DateOnly.FromDateTime(DateTime.Now) > DateOnly.FromDateTime(reservation.ReservationBegin) ||(DateOnly.FromDateTime(DateTime.Now) == DateOnly.FromDateTime(reservation.ReservationBegin) && TimeOnly.FromDateTime(reservation.ReservationEnd).AddMinutes(5) <= TimeOnly.FromDateTime(DateTime.Now)))
					{
						await dataManager.Reservations.DeleteReservationAsync(reservation);
					}
				}
				await EndOperation(stoppingToken);
			}
		}

		private async Task EndOperation(CancellationToken stoppingToken) => await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
	}
}
