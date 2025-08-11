using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AP_project.Models
{
    public class GuestBookContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;

        public GuestBookContext(DbContextOptions<GuestBookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}