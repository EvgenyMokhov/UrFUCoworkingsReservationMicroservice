using UrFUCoworkingsMicroservice.Business_Logic.Services;
using UrFUCoworkingsMicroservice.BusinessLogic.Services;

namespace UrFUCoworkingsMicroservice.Business_Logic
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
