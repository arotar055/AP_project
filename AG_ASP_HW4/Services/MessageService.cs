using AP_project.Models;
using AP_project.Repository;
using Microsoft.EntityFrameworkCore;

namespace AP_project.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepos;
        private readonly IUserRepository userRepos;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            messageRepos = messageRepository;
            userRepos = userRepository;
        }

        public async Task<List<MessageDto>> GetMessagesAsync()
        {
            var query = from m in messageRepos.Messages
                        join u in userRepos.Users on m.Id_User equals u.Id
                        orderby m.MessageDate descending
                        select new MessageDto
                        {
                            UserName = u.Name,
                            Text = m.MessageText,
                            Date = m.MessageDate
                        };
            return await query.ToListAsync();
        }

        public async Task AddMessageAsync(int userId, string messageText)
        {
            var msg = new Message
            {
                Id_User = userId,
                MessageText = messageText,
                MessageDate = DateTime.Now
            };
            await messageRepos.AddMessageAsync(msg);
            await messageRepos.SaveChangesAsync();
        }
    }
}