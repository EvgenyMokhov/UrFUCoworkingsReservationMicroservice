namespace UrFUCoworkingsMicroservice.Business_Logic.Services
{
    public class DeleteDataBackgroundService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public DeleteDataBackgroundService(IServiceProvider provider) => serviceProvider = provider;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
            }
        }
    }
}
