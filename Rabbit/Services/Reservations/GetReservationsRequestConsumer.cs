using MassTransit;
using UrFUCoworkingsMicroservice.Business_Logic;
using UrFUCoworkingsMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsMicroservice.Models.Responses.Reservations;

namespace UrFUCoworkingsMicroservice.Rabbit.Services.Reservations
{
    public class GetReservationsRequestConsumer : IConsumer<GetReservationsRequest>
    {
        private readonly ServiceManager serviceManager;
        public GetReservationsRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<GetReservationsRequest> context)
        {
            GetReservationsResponse response = new();
            response.ResponseData = await serviceManager.ReservationService.GetAllReservationsAsync();
            await context.RespondAsync(response);
        }
    }
}
