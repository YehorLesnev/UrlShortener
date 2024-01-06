using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;

namespace UrlShortener
{   
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(Constants.Constants.NumberOfCharsInShortLink);

                builder.HasIndex(s => s.Code).IsUnique();
            });
        }
    }
}
