using UrFUCoworkingsReservationMicroservice.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using UrFUCoworkingsModels.Data;
using UrFUCoworkingsModels.Data.Entities;

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
