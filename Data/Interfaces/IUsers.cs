using UrFUCoworkingsReservationMicroservice.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Data.Interfaces
{
    public interface IUsers
    {
        public Task<IEnumerable<User>> GetFilteredUsersAsync(string filter);
        public Task<User> GetUserAsync(Guid id);
    }
}
