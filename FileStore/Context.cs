using Microsoft.EntityFrameworkCore;
using FileStore.Configurations;
using FileStore.Models;
using FileStore.Data;

namespace FileStore
{
    public class Context:DbContext
    {
        public Context(DbContextOptions op):base(op) 
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<RealtimeQuote> RealtimeQuotes { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
