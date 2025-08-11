using AP_project.Models;

namespace AP_project.Repository
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        Task<User?> GetUserByNameAsync(string login);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}