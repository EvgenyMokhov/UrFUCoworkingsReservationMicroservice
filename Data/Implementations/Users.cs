using UrFUCoworkingsMicroservice.Data.Entities;
using UrFUCoworkingsMicroservice.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UrFUCoworkingsMicroservice.Data.Implementations
{
    internal class Users : IUsers
    {
        private readonly EFDBContext Context;
        public Users(EFDBContext context) => Context = context;
        public async Task<IEnumerable<User>> GetFilteredUsersAsync(string filter)
        {
            return Context.Users;
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateUserAsync(User user)
        {
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
        }
    }
}
