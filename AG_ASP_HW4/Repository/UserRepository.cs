using AP_project.Models;
using Microsoft.EntityFrameworkCore;

namespace AP_project.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GuestBookContext cont;
        public UserRepository(GuestBookContext context)
        {
            cont = context;
        }

        public IQueryable<User> Users => cont.Users;

        public async Task<User?> GetUserByNameAsync(string login)
        {
            return await cont.Users.FirstOrDefaultAsync(u => u.Name == login);
        }

        public async Task AddUserAsync(User user)
        {
            await cont.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await cont.SaveChangesAsync();
        }
    }
}