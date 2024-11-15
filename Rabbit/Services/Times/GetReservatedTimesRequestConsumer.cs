using MassTransit;
using UrFUCoworkingsMicroservice.Business_Logic;
using UrFUCoworkingsMicroservice.Models.Requests.Times;
using UrFUCoworkingsMicroservice.Models.Responses.Times;

namespace UrFUCoworkingsMicroservice.Rabbit.Services.Times
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
