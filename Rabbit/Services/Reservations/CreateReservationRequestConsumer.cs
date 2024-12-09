using MassTransit;
using UrFUCoworkingsModels.Responses.Reservations;
using UrFUCoworkingsReservationMicroservice.Business_Logic;
using CreateReservationRequest = UrFUCoworkingsModels.Requests.Reservations.CreateReservationRequest;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Reservations
{
    public class CreateReservationRequestConsumer : IConsumer<CreateReservationRequest>
    {
        private readonly ServiceManager serviceManager;
        public CreateReservationRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<CreateReservationRequest> context)
        {
            CreateReservationResponse response = new();
            response.ResponseData = await serviceManager.ReservationService.CreateReservationAsync(context.Message.RequestData, context.Message.Setting);
            await context.RespondAsync(response);
        }
    }
}
