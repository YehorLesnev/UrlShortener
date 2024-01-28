using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;

namespace UrlShortener
{   
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        public virtual DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(Constants.Constants.NumberOfCharsInShortLinkCode);
                builder.HasIndex(s => s.Code).IsUnique();

                builder.Property(s => s.ShortUrl).HasMaxLength(Constants.Constants.MaxLengthOfShortenedUrl);

                builder.Property(s => s.Code).UseCollation("SQL_Latin1_General_CP1_CS_AS");
                builder.Property(s => s.ShortUrl).UseCollation("SQL_Latin1_General_CP1_CS_AS");
                builder.Property(s => s.LongUrl).UseCollation("SQL_Latin1_General_CP1_CS_AS");
            });

            // for case sensitivity
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");
        }
    }
}
