using MassTransit;
using UrFUCoworkingsMicroservice.Business_Logic;
using UrFUCoworkingsMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsMicroservice.Models.Responses.Reservations;

namespace UrFUCoworkingsMicroservice.Rabbit.Services.Reservations
{
    public class DeleteReservationRequestConsumer : IConsumer<DeleteReservationRequest>
    {
        private readonly ServiceManager serviceManager;
        public DeleteReservationRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<DeleteReservationRequest> context)
        {
            await serviceManager.ReservationService.DeleteReservationAsync(context.Message.ReservationId);
            await context.RespondAsync(new DeleteReservationResponse());
        }
    }
}
