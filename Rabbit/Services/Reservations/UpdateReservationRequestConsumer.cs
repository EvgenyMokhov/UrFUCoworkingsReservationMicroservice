using MassTransit;
using UrFUCoworkingsMicroservice.Business_Logic;
using UrFUCoworkingsMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsMicroservice.Models.Responses.Reservations;

namespace UrFUCoworkingsMicroservice.Rabbit.Services.Reservations
{
    public class UpdateReservationRequestConsumer : IConsumer<UpdateReservationRequest>
    {
        private readonly ServiceManager serviceManager;
        public UpdateReservationRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<UpdateReservationRequest> context)
        {
            await serviceManager.ReservationService.UpdateReservationAsync(context.Message.RequestData);
            await context.RespondAsync(new UpdateReservationResponse());
        }
    }
}
