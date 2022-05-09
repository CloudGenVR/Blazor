using AppAutenticazione.Shared;

namespace AppAutenticazione.Client.Authentications.Abstractions
{
    public interface IAuthenticationService
    {
        Task<LoginUserResponseViewModel> Login(LoginViewModel model);
    }
}
