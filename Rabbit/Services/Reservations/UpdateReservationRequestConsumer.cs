using MassTransit;
using UrFUCoworkingsModels.Requests.Reservations;
using UrFUCoworkingsModels.Responses.Reservations;
using UrFUCoworkingsReservationMicroservice.Business_Logic;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Reservations
{
    public class UpdateReservationRequestConsumer : IConsumer<UpdateReservationRequest>
    {
        private readonly ServiceManager serviceManager;
        public UpdateReservationRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<UpdateReservationRequest> context)
        {
            UpdateReservationResponse response = new();
            response.ResponseData = await serviceManager.ReservationService.UpdateReservationAsync(context.Message.RequestData, context.Message.Setting);
            await context.RespondAsync(response);
        }
    }
}
