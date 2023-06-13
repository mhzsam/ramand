using Application.DTO;


namespace Application.Services
{
    public interface IAuthenticateService
    {
        Task<string> Login(UserLogin user);
    }
}
