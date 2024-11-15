using UrFUCoworkingsMicroservice.Data.Entities;
using UrFUCoworkingsMicroservice.Data.Interfaces;
using UrFUCoworkingsMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace UrFUCoworkingsMicroservice.Data.Implementations
{
    internal class Visits : IVisits
    {
        private readonly EFDBContext Context;
        public Visits(EFDBContext context) => Context = context;

        public async Task<IEnumerable<Visit>> GetAllVisitsAsync()
        {
            return await Context.Visits.ToListAsync();
        }

        public async Task<Visit> GetVisitAsync(Guid guid)
        {
            return await Context.Visits.FirstOrDefaultAsync(x => x.Id == guid);
        }

        public async Task<Visit> GetVisitAsync(Guid userId, Guid reservationId)
        {
            return await Context.Visits.FirstOrDefaultAsync(x => x.UserId == userId && x.ReservationId == reservationId);
        }

        public async Task UpdateVisitAsync(Visit visitor)
        {
            Context.Visits.Add(visitor);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Visit>> GetVisitsByReservationIdAsync(Guid reservationId)
        {
            return await Context.Visits.Where(visit => visit.ReservationId == reservationId).ToListAsync();
        }
        public async Task CreateVisitAsync(Visit visit)
        {
            await Context.Visits.AddAsync(visit);
            await Context.SaveChangesAsync();
        }
    }
}
