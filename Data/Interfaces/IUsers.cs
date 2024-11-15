using UrFUCoworkingsMicroservice.Data.Entities;

namespace UrFUCoworkingsMicroservice.Data.Interfaces
{
    public interface IUsers
    {
        public Task<IEnumerable<User>> GetFilteredUsersAsync(string filter);
        public Task<User> GetUserAsync(Guid id);
        public Task CreateUserAsync(User user);
    }
}
