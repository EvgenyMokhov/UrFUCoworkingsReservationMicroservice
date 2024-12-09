using UrFUCoworkingsModels.Data.Entities;

namespace UrFUCoworkingsReservationMicroservice.Data.Interfaces
{
    public interface IPlaces
    {
        public Task<Place> GetPlaceAsync(Guid id);
    }
}
