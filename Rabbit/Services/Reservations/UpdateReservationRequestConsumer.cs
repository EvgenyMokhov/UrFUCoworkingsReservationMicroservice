using MassTransit;
using UrFUCoworkingsReservationMicroservice.Business_Logic;
using UrFUCoworkingsReservationMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsReservationMicroservice.Models.Responses.Reservations;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Reservations
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
