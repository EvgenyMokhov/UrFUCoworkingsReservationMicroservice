using UrFUCoworkingsReservationMicroservice.Data.Interfaces;

namespace UrFUCoworkingsReservationMicroservice.Data
{
    public class DataManager
    {
        public IUsers Users { get; set; }
        public IVisits Visits { get; set; }
        public IReservations Reservations { get; set; }
        public IPlaces Places { get; set; }
        public DataManager(IServiceProvider provider)
        {
            Places = provider.GetRequiredService<IPlaces>();
            Visits = provider.GetRequiredService<IVisits>();
            Users = provider.GetRequiredService<IUsers>();
            Reservations = provider.GetRequiredService<IReservations>();
        }
    }
}
