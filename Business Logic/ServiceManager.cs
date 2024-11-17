using UrFUCoworkingsReservationMicroservice.Business_Logic.Services;
using UrFUCoworkingsReservationMicroservice.BusinessLogic.Services;
using UrFUCoworkingsReservationMicroservice.Data;

namespace UrFUCoworkingsReservationMicroservice.Business_Logic
{
    public class ServiceManager
    {
        public ReservationService ReservationService { get; set; }
        public UserService UserService { get; set; }
        public VisitService VisitorService { get; set; }
        public ServiceManager(IServiceProvider provider) 
        {
            ReservationService = new(provider);
            UserService = new(provider);
            VisitorService = new(provider);
        }
    }
}
