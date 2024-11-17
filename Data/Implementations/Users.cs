using UrFUCoworkingsReservationMicroservice.Data.Entities;
using UrFUCoworkingsReservationMicroservice.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace UrFUCoworkingsReservationMicroservice.Data.Implementations
{
    internal class Users : IUsers
    {
        private readonly EFDBContext Context;
        public Users(EFDBContext context) => Context = context;
        public async Task<IEnumerable<User>> GetFilteredUsersAsync(string filter)
        {
            if (filter == null || filter == "")
                return await Context.Users.ToListAsync();
            return await Context.Users.FromSqlRaw($"SELECT * FROM dbo.Users WHERE Name LIKE '@filter%'", new SqlParameter("@filter", filter)).ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
