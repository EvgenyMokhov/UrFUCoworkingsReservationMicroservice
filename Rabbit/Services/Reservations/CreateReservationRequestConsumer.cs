using MassTransit;
using UrFUCoworkingsReservationMicroservice.Business_Logic;
using UrFUCoworkingsReservationMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsReservationMicroservice.Models.Responses.Reservations;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Reservations
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
