using AP_project.Models;

namespace AP_project.Services
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetMessagesAsync();
        Task AddMessageAsync(int userId, string messageText);
    }
}