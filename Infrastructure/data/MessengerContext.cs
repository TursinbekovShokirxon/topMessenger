using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.data
{
    public class MessengerContext:DbContext
    {
        public MessengerContext(DbContextOptions<MessengerContext> options):base(options)
        {
            

        }

        public DbSet<ChatMessage> Messages{ get; set; }
        public DbSet<ChatUser> Users { get; set; }
    }
}
