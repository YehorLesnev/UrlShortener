using UrlShortener.Entities;

namespace UrlShortener.Tests.TestDataHelper
{
    public class TestDataHelper
    {
        public static List<ShortenedUrl> GetShortenedUrls()
        {
            return new List<ShortenedUrl>()
            {
                    new()
                    {
                        Id = new Guid("08c4abea-d4a1-47a8-bb47-027c789ec567"),
                        LongUrl = "https://www.youtube.com/",
                        Code = "MVDgnKe",
                        ShortUrl = $"https://localhost:5001/api/MVDgnKe",
                        CreatedOnUtc = DateTime.Parse("2024-01-22 14:59:43.3859820")
                    },

                    new()
                    {
                        Id = new Guid("c1ae7f05-2373-4dbb-b5e0-4ca857ea777e"),
                        LongUrl = "https://mail.google.com/mail/u/0/#inbox",
                        Code = "xCktkmi",
                        ShortUrl = $"https://localhost:5001/api/xCktkmi",
                        CreatedOnUtc = DateTime.Parse("2024-01-22 14:59:43.3859820")
                    },

                    new()
                    {
                        Id = new Guid("fe00d3ba-49da-4d32-9588-696501792977"),
                        LongUrl = "https://www.google.com/maps",
                        Code = "Gt2K18p",
                        ShortUrl = $"https://localhost:5001/api/Gt2K18p",
                        CreatedOnUtc = DateTime.Parse("2024-01-22 14:59:43.3859820")
                    },

                    new()
                    {
                        Id = new Guid("b8a5a092-a868-4620-a6dd-767bd261ed51"),
                        LongUrl = "https://drive.google.com/drive/u/0/home",
                        Code = "51m4S2J",
                        ShortUrl = $"https://localhost:5001/api/51m4S2J",
                        CreatedOnUtc = DateTime.Parse("2024-01-22 14:59:43.3859820")
                    },

                    new()
                    {
                        Id = new Guid("3713bc7a-35d1-4e4f-b95d-3b6aa2830955"),
                        LongUrl = "https://github.com/",
                        Code = "TuxBN9A",
                        ShortUrl = $"https://localhost:5001/api/TuxBN9A",
                        CreatedOnUtc = DateTime.Parse("2024-01-22 14:59:43.3859820")
                    },
            };
        }
    }
}
