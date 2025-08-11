using AP_project.Models;
using AP_project.Repository;

namespace AP_project.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepos;
        private readonly IHashService hashServ;

        public AccountService(IUserRepository userRepository, IHashService hashService)
        {
            userRepos = userRepository;
            hashServ = hashService;
        }

        public async Task<User?> ValidateUserAsync(LoginModel model)
        {
            var user = await userRepos.GetUserByNameAsync(model.Login);
            if (user == null)
                return null;

            string salt = user.Salt ?? string.Empty;
            string computedHash = hashServ.ComputeHash(salt, model.Password);
            if (!string.Equals(user.Pwd, computedHash, StringComparison.OrdinalIgnoreCase))
                return null;
            return user;
        }

        public async Task RegisterUserAsync(RegisterModel model)
        {
            if (userRepos.Users.Any(u => u.Name == model.Login))
                throw new Exception("Такой логин уже существует");

            var user = new User
            {
                Name = model.Login
            };

            string salt = hashServ.GenerateSalt();
            string hash = hashServ.ComputeHash(salt, model.Password);
            user.Salt = salt;
            user.Pwd = hash;

            await userRepos.AddUserAsync(user);
            await userRepos.SaveChangesAsync();
        }
    }
}