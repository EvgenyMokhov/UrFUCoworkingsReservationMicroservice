using MassTransit;
using UrFUCoworkingsReservationMicroservice.Business_Logic;
using UrFUCoworkingsReservationMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsReservationMicroservice.Models.Responses.Reservations;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Reservations
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
