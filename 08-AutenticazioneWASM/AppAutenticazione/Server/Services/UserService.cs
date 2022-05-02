namespace AppAutenticazione.Server.Services
{
    public interface IUserService
    {
        Task<bool> LoginAsync(string username, string password);
    }

    public class UserService : IUserService
    {
        public Task<bool> LoginAsync(string username, string password)
        {
            // TODO logica del database
            return Task.FromResult(true);
        }
    }
}
