
using DotBot.Data.Models;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace DotBot.Data
{
    public class SqlDbContext : DbContext
    {
        public DbSet<WordSuggestion> WordSuggestions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=DotBotDb;Trusted_Connection=True;");
        }
    }
}