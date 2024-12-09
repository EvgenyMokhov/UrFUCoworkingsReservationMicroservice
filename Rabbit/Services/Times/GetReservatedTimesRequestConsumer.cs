using MassTransit;
using UrFUCoworkingsModels.Requests.Times;
using UrFUCoworkingsModels.Responses.Times;
using UrFUCoworkingsReservationMicroservice.Business_Logic;

namespace UrFUCoworkingsReservationMicroservice.Rabbit.Services.Times
{
    public class GetReservatedTimesRequestConsumer : IConsumer<GetReservatedTimesRequest>
    {
        private readonly ServiceManager serviceManager;
        public GetReservatedTimesRequestConsumer(IServiceProvider provider) => serviceManager = new(provider);
        public async Task Consume(ConsumeContext<GetReservatedTimesRequest> context)
        {
            GetReservatedTimesResponse response = new();
            response.ResponseData = await serviceManager.ReservationService.GetReservatedIntervalsAsync(context.Message.PlaceId, context.Message.Date, context.Message.Setting);
            await context.RespondAsync(response);
        }
    }
}
