using UrFUCoworkingsMicroservice.Business_Logic.Services;
using UrFUCoworkingsMicroservice.Data;
using UrFUCoworkingsMicroservice.Data.Entities;
using UrFUCoworkingsMicroservice.Models.DTOs;
using System.Globalization;

namespace UrFUCoworkingsMicroservice.BusinessLogic.Services
{
    public class ReservationService
    {
        private readonly IServiceProvider serviceProvider;
        public ReservationService(IServiceProvider provider) => serviceProvider = provider;

        public async Task<List<ReservationViewModel>> GetAllReservationsAsync()
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            IEnumerable<Reservation> reservations = await dataManager.Reservations.GetAllReservationsAsync();
            return (await Task.WhenAll(reservations.Select(async reservation => await ReservationToView(reservation)))).ToList();
        }

        public async Task<ReservationViewModel> ReservationToView(Reservation reservation)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            ReservationViewModel viewModel = new ReservationViewModel();
            viewModel.ReservationBegin = reservation.ReservationBegin.ToString("g", new CultureInfo("ru-RU"));
            viewModel.ReservationEnd = reservation.ReservationEnd.ToString("g", new CultureInfo("ru-RU"));
            viewModel.ReservatorName = (await dataManager.Users.GetUserAsync(reservation.ReservatorId)).Name;
            viewModel.Visitors = new();
            viewModel.Places = new();
            foreach (Visit visitor in reservation.Visits)
                viewModel.Visitors.Add(visitor.User.Name);
            foreach (Place place in reservation.Places)
                viewModel.Places.Add(place.Id);
            return viewModel;
        }

        public async Task<List<(TimeOnly reservationBegin, TimeOnly reservationEnd)>> GetReservatedIntervalsAsync(Guid placeId, DateOnly date)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            List<List<DateTime>> result = new();
            IEnumerable<Reservation> reservations = await dataManager.Reservations.GetReservationsOnDateAsync(placeId, date);
            List<DateTime> reservationBegins = reservations.Select(res => res.ReservationBegin).OrderBy(date => date).ToList();
            List<DateTime> reservationEnds = reservations.Select(res => res.ReservationEnd).OrderBy(date => date).ToList();
            List<(TimeOnly reservationBegin, TimeOnly reservationEnd)> reservatedIntervals = new();
            for (int i = 0; i < reservationBegins.Count; i++)
                reservatedIntervals.Add(new(TimeOnly.FromDateTime(reservationBegins[i]), TimeOnly.FromDateTime(reservationEnds[i])));
            return reservatedIntervals;
        }

        public async Task UpdateReservationAsync(ReservationEditModel reservation)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            Reservation dbReservation = await dataManager.Reservations.GetReservationAsync(reservation.ReservationId);
            DateTime resB = new();
            if (DateTime.TryParse(reservation.ReservationBegin, out resB))
            { dbReservation.ReservationBegin = resB; }
            DateTime resE = new();
            if (DateTime.TryParse(reservation.ReservationEnd, out resE))
            { dbReservation.ReservationEnd = resE; }
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
            foreach(Guid userId in reservation.UserIds)
                if (!liveVisitsUserIds.Contains(userId))
                    dbReservation.Visits.Add(await visitService.CreateVisitAsync(userId, dbReservation.Id));
        }

        public async Task CreateReservationAsync(ReservationEditModel reservation)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            Reservation dbReservation = new();
            dbReservation.Id = Guid.NewGuid();
            dbReservation.ReservatorId = reservation.ReservatorId;
            DateTime resB = new();
            if (DateTime.TryParse(reservation.ReservationBegin, out resB))
            { dbReservation.ReservationBegin = resB; }
            DateTime resE = new();
            if (DateTime.TryParse(reservation.ReservationEnd, out resE))
            { dbReservation.ReservationEnd = resE; }
            dbReservation.Places = (await Task.WhenAll(reservation.PlacesIds.Select(async placeId => await dataManager.Places.GetPlaceAsync(placeId)))).ToList();
            VisitService visitService = new(serviceProvider);
            dbReservation.Visits = (await Task.WhenAll(reservation.UserIds.Select(async userId => await visitService.CreateVisitAsync(userId, dbReservation.Id)))).ToList();
        }

        public async Task DeleteReservationAsync(Guid reservationId)
        {
            
        }

        public async Task<ReservationEditModel> GetReservationByIdAsync(Guid id)
        {
            return new();
        }
    }
}
