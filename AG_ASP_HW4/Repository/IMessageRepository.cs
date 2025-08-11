using AP_project.Models;

namespace AP_project.Repository
{
    public interface IMessageRepository
    {
        IQueryable<Message> Messages { get; }
        Task AddMessageAsync(Message message);
        Task SaveChangesAsync();
    }
}