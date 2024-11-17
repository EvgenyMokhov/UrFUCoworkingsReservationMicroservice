using MassTransit;
using UrFUCoworkingsReservationMicroservice.Business_Logic;
using UrFUCoworkingsReservationMicroservice.Models.Requests.Times;
using UrFUCoworkingsReservationMicroservice.Models.Responses.Times;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Times
{
    public class GetReservatedTimesRequestConsumer : IConsumer<GetReservatedTimesRequest>
    {
        private readonly ServiceManager serviceManager;
        public GetReservatedTimesRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<GetReservatedTimesRequest> context)
        {
            GetReservatedTimesResponse response = new();
            response.ResponseData = await serviceManager.ReservationService.GetReservatedIntervalsAsync(context.Message.PlaceId, context.Message.Date);
            await context.RespondAsync(response);
        }
    }
}
