using UrFUCoworkingsMicroservice.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrFUCoworkingsMicroservice.Data.Interfaces
{
    public interface IPlaces
    {
        public Task<IEnumerable<Place>> GetAllPlacesAsync();
        public Task<Place> GetPlaceAsync(Guid id);
    }
}
