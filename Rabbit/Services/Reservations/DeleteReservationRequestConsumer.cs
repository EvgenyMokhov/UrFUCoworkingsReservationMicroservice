using MassTransit;
using UrFUCoworkingsModels.Requests.Reservations;
using UrFUCoworkingsModels.Responses.Reservations;
using UrFUCoworkingsReservationMicroservice.Business_Logic;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Reservations
{
    public class DeleteReservationRequestConsumer : IConsumer<DeleteReservationRequest>
    {
        private readonly ServiceManager serviceManager;
        public DeleteReservationRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<DeleteReservationRequest> context)
        {
            DeleteReservationResponse response = new();
            response.ResponseData = await serviceManager.ReservationService.DeleteReservationAsync(context.Message.ReservationId);
            await context.RespondAsync(response);
        }
    }
}
