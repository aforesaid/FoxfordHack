using FoxfordHack.Models.DataBaseModels;
using FoxfordHack.Services.DataBase;
using Microsoft.EntityFrameworkCore;
namespace FoxfordHack.DataBase.DBApplication
{
    public class ApplicationDbContext : DbContext
    {
        private string ConnectionString { get; set; }
        public DbSet<Course> Courses { get; set; }
        public ApplicationDbContext()
        {
            ConnectionString = new SettingsDB().GetConnectionString();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(ConnectionString);
    }
}