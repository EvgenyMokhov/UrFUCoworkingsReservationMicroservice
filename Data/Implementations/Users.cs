using UrFUCoworkingsReservationMicroservice.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using UrFUCoworkingsModels.Data;
using UrFUCoworkingsModels.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Data.Implementations
{
    internal class Users : IUsers
    {
        private readonly EFDBContext Context;
        public Users(EFDBContext context) => Context = context;

        public async Task<User> GetUserAsync(Guid id)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
