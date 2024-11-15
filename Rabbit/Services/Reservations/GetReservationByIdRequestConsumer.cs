using MassTransit;
using UrFUCoworkingsMicroservice.Business_Logic;
using UrFUCoworkingsMicroservice.Models.Requests.Reservations;
using UrFUCoworkingsMicroservice.Models.Responses.Reservations;

namespace UrFUCoworkingsMicroservice.Rabbit.Services.Reservations
{
    public class GetReservationByIdRequestConsumer : IConsumer<GetReservationByIdRequest>
    {
        private readonly ServiceManager serviceManager;
        public GetReservationByIdRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<GetReservationByIdRequest> context)
        {
            GetReservationByIdResponse response = new();
            response.ResponseData = await serviceManager.ReservationService.GetReservationByIdAsync(context.Message.ReservationId);
            await context.RespondAsync(response);
        }
    }
}
