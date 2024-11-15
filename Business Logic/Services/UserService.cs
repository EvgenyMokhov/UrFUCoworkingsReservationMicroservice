using UrFUCoworkingsMicroservice.Data;
using UrFUCoworkingsMicroservice.Data.Entities;

namespace UrFUCoworkingsMicroservice.Business_Logic.Services
{
    public class UserService
    {
        private readonly IServiceProvider serviceProvider;
        public UserService(IServiceProvider provider) => serviceProvider = provider;

        public async Task<List<User>> GetFilteredUsers(string filter)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            DataManager dataManager = new(serviceProvider);
            return (await dataManager.Users.GetFilteredUsersAsync(filter)).ToList();
        }
    }
}
