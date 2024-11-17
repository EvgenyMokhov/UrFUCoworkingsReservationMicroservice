using UrFUCoworkingsReservationMicroservice.Data.Interfaces;
using UrFUCoworkingsReservationMicroservice.Data.Entities;
using UrFUCoworkingsReservationMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace UrFUCoworkingsReservationMicroservice.Data.Implementations
{
    internal class Reservations : IReservations
    {
        private readonly EFDBContext Context;
        public Reservations(EFDBContext context) => Context = context;
        public async Task DeleteReservationAsync(Reservation reservation)
        {
            if (reservation != null)
                Context.Reservations.Remove(reservation);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await Context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservationAsync(Guid id)
        {
            return await Context.Reservations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            Context.Entry(reservation).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsOnDateAsync(Guid placeId, DateOnly date)
        {
            return await Context.Reservations.Where(res => DateOnly.FromDateTime(res.ReservationBegin) == date && res.Places.Select(place => place.Id).Contains(placeId)).ToListAsync();
        }

        public async Task CreateReservationAsync(Reservation reservation)
        {
            await Context.Reservations.AddAsync(reservation);
            await Context.SaveChangesAsync();
        }
    }
}
