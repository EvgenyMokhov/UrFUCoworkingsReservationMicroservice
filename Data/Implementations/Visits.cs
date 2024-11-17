using UrFUCoworkingsReservationMicroservice.Data.Entities;
using UrFUCoworkingsReservationMicroservice.Data.Interfaces;
using UrFUCoworkingsReservationMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace UrFUCoworkingsReservationMicroservice.Data.Implementations
{
    internal class Visits : IVisits
    {
        private readonly EFDBContext Context;
        public Visits(EFDBContext context) => Context = context;

        public async Task<IEnumerable<Visit>> GetVisitsByReservationIdAsync(Guid reservationId)
        {
            return await Context.Visits.Where(visit => visit.ReservationId == reservationId).ToListAsync();
        }

        public async Task CreateVisitAsync(Visit visit)
        {
            await Context.Visits.AddAsync(visit);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteVisitAsync(Visit visit)
        {
            if (visit != null)
                Context.Remove(visit);
            await Context.SaveChangesAsync();
        }
    }
}
