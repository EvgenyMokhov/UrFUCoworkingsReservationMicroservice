using UrFUCoworkingsReservationMicroservice.Data.Interfaces;
using UrFUCoworkingsReservationMicroservice.Data.Entities;
using UrFUCoworkingsReservationMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace UrFUCoworkingsReservationMicroservice.Data.Implementations
{
    internal class Places : IPlaces
    {
        private readonly EFDBContext Context;
        public Places(EFDBContext context) => Context = context;

        public async Task<Place> GetPlaceAsync(Guid id)
        {
            return await Context.Places.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
