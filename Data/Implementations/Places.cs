using UrFUCoworkingsMicroservice.Data.Interfaces;
using UrFUCoworkingsMicroservice.Data.Entities;
using UrFUCoworkingsMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace UrFUCoworkingsMicroservice.Data.Implementations
{
    internal class Places : IPlaces
    {
        private readonly EFDBContext Context;
        public Places(EFDBContext context) => Context = context;
        public async Task<IEnumerable<Place>> GetAllPlacesAsync()
        {
            return await Context.Places.ToListAsync();
        }

        public async Task<Place> GetPlaceAsync(Guid id)
        {
            return await Context.Places.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
