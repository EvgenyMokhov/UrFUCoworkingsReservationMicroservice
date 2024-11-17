using MassTransit;
using UrFUCoworkingsReservationMicroservice.Business_Logic;
using UrFUCoworkingsReservationMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsReservationMicroservice.Models.Responses.Reservations;

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
