using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using UrlShortener.Entities;
using UrlShortener.Services;

namespace UrlShortener.Tests
{
    [TestClass]
    public class TestUrlShorteningService
    {
        [TestMethod]
        public async Task Test_GenerateUniqueCode()
        {
            // Arrange
            var applicationDbContextMock = new Mock<ApplicationDbContext>();
            applicationDbContextMock.Setup<DbSet<ShortenedUrl>>(x => x.ShortenedUrls)
                .ReturnsDbSet(TestDataHelper.TestDataHelper.GetShortenedUrls());

            var existingCodes = TestDataHelper.TestDataHelper.GetShortenedUrls()
                .Select(x => x.Code);

            // Act
            UrlShorteningService urlShorteningService = new(applicationDbContextMock.Object);
            var code = await urlShorteningService.GenerateUniqueCode();

            // Assert
            Assert.IsNotNull(code);
            Assert.IsTrue(code.Length == Constants.Constants.NumberOfCharsInShortLink);
            Assert.IsFalse(existingCodes.Contains(code));
        }
    }
}