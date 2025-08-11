using AP_project.Models;
using Microsoft.EntityFrameworkCore;

namespace AP_project.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly GuestBookContext cont;
        public MessageRepository(GuestBookContext context)
        {
            cont = context;
        }

        public IQueryable<Message> Messages => cont.Messages;

        public async Task AddMessageAsync(Message message)
        {
            await cont.Messages.AddAsync(message);
        }

        public async Task SaveChangesAsync()
        {
            await cont.SaveChangesAsync();
        }
    }
}