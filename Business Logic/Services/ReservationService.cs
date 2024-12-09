using UrFUCoworkingsReservationMicroservice.Business_Logic.Services;
using UrFUCoworkingsReservationMicroservice.Data;
using System.Globalization;
using UrFUCoworkingsModels.DTOs;
using UrFUCoworkingsModels.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.BusinessLogic.Services
{
    public class ReservationService
    {
        private readonly IServiceProvider serviceProvider;
        public ReservationService(IServiceProvider provider) => serviceProvider = provider;

        public async Task<List<ReservationView>> GetAllReservationsAsync(UserDTO user)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
            IEnumerable<Reservation> reservations = await dataManager.Reservations.GetUserReservationsAsync(user.Id);
            return reservations.Select(reservation => ReservationToView(reservation, user)).ToList();
        }

        private ReservationView ReservationToView(Reservation reservation, UserDTO user)
        {
            ReservationView viewModel = new ReservationView();
            viewModel.ReservationId = reservation.Id;
            viewModel.ReservationBegin = reservation.ReservationBegin.ToString("g", new CultureInfo("ru-RU"));
            viewModel.ReservationEnd = reservation.ReservationEnd.ToString("g", new CultureInfo("ru-RU"));
            viewModel.ReservatorName = user.Name;
            viewModel.Visitors = new();
            viewModel.Places = new();
            foreach (Visit visit in reservation.Visits)
                viewModel.Visitors.Add(visit.User.Name);
            foreach (Place place in reservation.Places)
                viewModel.Places.Add(place.Id);
            return viewModel;
        }

        private ReservationEdit ReservationToEdit(Reservation reservation)
        {
            ReservationEdit editModel = new();
            editModel.ReservationId = reservation.Id;
            editModel.ReservatorId = reservation.ReservatorId;
            editModel.ReservationDay = DateOnly.FromDateTime(reservation.ReservationBegin);
            editModel.ReservationBegin = TimeOnly.FromDateTime(reservation.ReservationBegin);
            editModel.ReservationEnd = TimeOnly.FromDateTime(reservation.ReservationEnd);
            editModel.PlacesIds = reservation.Places.Select(place => place.Id).ToList();
            editModel.UserIds = reservation.Visits.Select(visit => visit.UserId).ToList();
            return editModel;
        }

        public async Task<List<(TimeOnly reservationBegin, TimeOnly reservationEnd)>> GetReservatedIntervalsAsync(Guid placeId, DateOnly date, CSDTO settings)
        {
            if (!settings.IsWorking)
                return new() { new() { reservationBegin = settings.Opening, reservationEnd = settings.Closing } };
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
            List<List<DateTime>> result = new();
            IEnumerable<Reservation> reservations = await dataManager.Reservations.GetReservationsOnDateAsync(placeId, date);
            List<DateTime> reservationBegins = reservations.Select(res => res.ReservationBegin).OrderBy(date => date).ToList();
            List<DateTime> reservationEnds = reservations.Select(res => res.ReservationEnd).OrderBy(date => date).ToList();
            List<(TimeOnly reservationBegin, TimeOnly reservationEnd)> reservatedIntervals = new();
            for (int i = 0; i < reservationBegins.Count; i++)
                reservatedIntervals.Add(new(TimeOnly.FromDateTime(reservationBegins[i]), TimeOnly.FromDateTime(reservationEnds[i])));
            return reservatedIntervals;
        }

        public async Task<ReservationEdit> UpdateReservationAsync(ReservationEdit reservation, CSDTO settings)
        {
            if (settings.IsWorking && settings.Opening <= reservation.ReservationBegin && settings.Closing >= reservation.ReservationEnd)
            {
                using IServiceScope scope = serviceProvider.CreateScope();
                DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
                Reservation dbReservation = await dataManager.Reservations.GetReservationAsync(reservation.ReservationId);
                dbReservation.ReservationBegin = new(reservation.ReservationDay, reservation.ReservationBegin);
                dbReservation.ReservationEnd = new(reservation.ReservationDay, reservation.ReservationEnd);
                foreach (Place place in dbReservation.Places.ToList())
                    if (!reservation.PlacesIds.Contains(place.Id))
                        dbReservation.Places.Remove(place);
                IEnumerable<Guid> livePlacesIds = dbReservation.Places.Select(place => place.Id);
                foreach (Guid placeId in reservation.PlacesIds)
                    if (!livePlacesIds.Contains(placeId))
                        dbReservation.Places.Add(await dataManager.Places.GetPlaceAsync(placeId));
                VisitService visitService = new(serviceProvider);
                foreach (Visit visit in dbReservation.Visits.ToList())
                    if (!reservation.UserIds.Contains(visit.UserId))
                    {
                        dbReservation.Visits.Remove(visit);
                        await visitService.DeleteVisitAsync(visit);
                    }
                IEnumerable<Guid> liveVisitsUserIds = dbReservation.Visits.Select(visits => visits.UserId);
                foreach (Guid userId in reservation.UserIds)
                    if (!liveVisitsUserIds.Contains(userId))
                        dbReservation.Visits.Add(await visitService.CreateVisitAsync(userId, dbReservation.Id));
                await dataManager.Reservations.UpdateReservationAsync(dbReservation);
                return reservation;
            }
            else throw new ArgumentException();
        }

        public async Task<ReservationEdit> CreateReservationAsync(ReservationEdit reservation, CSDTO settings)
        {
            if (settings.IsWorking && settings.Opening <= reservation.ReservationBegin && settings.Closing >= reservation.ReservationEnd)
            {
                using IServiceScope scope = serviceProvider.CreateScope();
                DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
                Reservation dbReservation = new();
                dbReservation.Id = Guid.NewGuid();
                dbReservation.ReservatorId = reservation.ReservatorId;
                dbReservation.ReservationBegin = new(reservation.ReservationDay, reservation.ReservationBegin);
                dbReservation.ReservationEnd = new(reservation.ReservationDay, reservation.ReservationEnd);
                await dataManager.Reservations.CreateReservationAsync(dbReservation);
                dbReservation.Places = (await Task.WhenAll(reservation.PlacesIds.Select(async placeId => await dataManager.Places.GetPlaceAsync(placeId)))).ToList();
                VisitService visitService = new(serviceProvider);
                dbReservation.Visits = (await Task.WhenAll(reservation.UserIds.Select(async userId => await visitService.CreateVisitAsync(userId, dbReservation.Id)))).ToList();
                await dataManager.Reservations.UpdateReservationAsync(dbReservation);
                return reservation;
            }
            else throw new ArgumentException();
        }

        public async Task<ReservationEdit> DeleteReservationAsync(Guid reservationId)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
            Reservation dbReservation = await dataManager.Reservations.GetReservationAsync(reservationId);
            await dataManager.Reservations.DeleteReservationAsync(dbReservation);
            return ReservationToEdit(dbReservation);
        }

        public async Task<ReservationEdit> GetReservationByIdAsync(Guid id)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = scope.ServiceProvider.GetRequiredService<DataManager>();
            return ReservationToEdit(await dataManager.Reservations.GetReservationAsync(id)); 
        }
    }
}
