using AP_project.Models;

namespace AP_project.Services
{
    public interface IAccountService
    {
        Task<User?> ValidateUserAsync(LoginModel model);
        Task RegisterUserAsync(RegisterModel model);
    }
}