using Microsoft.EntityFrameworkCore;


namespace userauthapi
{
    

        public class ConsumerContext : DbContext
        {
            public ConsumerContext(DbContextOptions<ConsumerContext> options) : base(options) { }

            public DbSet<Consumer> Consumers { get; set; }
        }
    }


