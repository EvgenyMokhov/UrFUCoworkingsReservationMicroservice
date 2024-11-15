using MassTransit;
using UrFUCoworkingsMicroservice.Business_Logic;
using UrFUCoworkingsMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsMicroservice.Models.Responses.Reservations;

namespace UrFUCoworkingsMicroservice.Rabbit.Services.Reservations
{
    public class CreateReservationRequestConsumer : IConsumer<CreateReservationRequest>
    {
        private readonly ServiceManager serviceManager;
        public CreateReservationRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<CreateReservationRequest> context)
        {
            await serviceManager.ReservationService.CreateReservationAsync(context.Message.RequestData);
            await context.RespondAsync(new CreateReservationResponse());
        }
    }
}
