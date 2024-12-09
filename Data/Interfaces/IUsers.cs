using UrFUCoworkingsModels.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Data.Interfaces
{
    public interface IUsers
    {
        public Task<User> GetUserAsync(Guid id);
    }
}
