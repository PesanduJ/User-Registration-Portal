using Microsoft.EntityFrameworkCore;

namespace UserCrud.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-IONC2G5;Initial Catalog=crud;Integrated Security=True; TrustServerCertificate=True");
        }
    }
}
