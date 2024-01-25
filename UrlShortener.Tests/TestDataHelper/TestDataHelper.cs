using UrlShortener.Entities;

namespace UrlShortener.Tests.TestDataHelper
{
    public class TestDataHelper
    {
        public static List<ShortenedUrl> GetShortenedUrls()
        {
            return new List<ShortenedUrl>()
            {
                    new(
                        new Guid("08c4abea-d4a1-47a8-bb47-027c789ec567"),
                        "https://www.youtube.com/",
                        $"https://localhost:5001/api/MVDgnKe",
                        "MVDgnKe",
                        DateTime.Parse("2024-01-22 14:59:43.3859820")
                        ),

                    new(
                        new Guid("c1ae7f05-2373-4dbb-b5e0-4ca857ea777e"),
                        "https://mail.google.com/mail/u/0/#inbox",
                        $"https://localhost:5001/api/xCktkmi",
                        "xCktkmi",
                        DateTime.Parse("2024-01-22 14:59:43.3859820")
                        ),

                    new(
                        new Guid("fe00d3ba-49da-4d32-9588-696501792977"),
                        "https://www.google.com/maps",
                        $"https://localhost:5001/api/Gt2K18p",
                        "Gt2K18p",
                        DateTime.Parse("2024-01-22 14:59:43.3859820")
                        ),

                    new(
                        new Guid("b8a5a092-a868-4620-a6dd-767bd261ed51"),
                        "https://drive.google.com/drive/u/0/home",
                        $"https://localhost:5001/api/51m4S2J",
                        "51m4S2J",
                        DateTime.Parse("2024-01-22 14:59:43.3859820")
                        ),

                    new(
                        new Guid("3713bc7a-35d1-4e4f-b95d-3b6aa2830955"),
                        "https://github.com/",
                        $"https://localhost:5001/api/TuxBN9A",
                        "TuxBN9A",
                        DateTime.Parse("2024-01-22 14:59:43.3859820")
                        ),
            };
        }

        public static List<string> GetValidUrls()
        {
            return new List<string>()
            {
                "https://www.linkedin.com/in/yehor-lesnevych-130640158/",
                "https://github.com/YehorLesnev",
                "http://www.testingmcafeesites.com/",
                "http://www.testingmcafeesites.com/testreputation_highrisk.html",
            };
        }

        public static List<string> GetInvalidUrls()
        {
            return new List<string>()
            {
                "github.com/YehorLesnev",
                "www.linkedin.com/in/yehor-lesnevych-130640158/",
                "invalid",
                "invalid string",
                ""
            };
        }
    }
}
