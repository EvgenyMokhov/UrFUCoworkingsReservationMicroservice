using MassTransit;
using UrFUCoworkingsModels.Requests.Reservations;
using UrFUCoworkingsModels.Responses.Reservations;
using UrFUCoworkingsReservationMicroservice.Business_Logic;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Reservations
{
    public class GetReservationsRequestConsumer : IConsumer<GetReservationsRequest>
    {
        private readonly ServiceManager serviceManager;
        public GetReservationsRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<GetReservationsRequest> context)
        {
            GetReservationsResponse response = new();
            response.ResponseData = await serviceManager.ReservationService.GetAllReservationsAsync(context.Message.User);
            await context.RespondAsync(response);
        }
    }
}
