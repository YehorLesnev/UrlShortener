using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using UrlShortener.Entities;
using UrlShortener.Repositories.Implementations;
using UrlShortener.Repositories.Interfaces;
using UrlShortener.Services;
using UrlShortener.Services.Static;

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

            var shortenedUrlRepositoryMock = new Mock<IShortenedUrlRepository>();
            shortenedUrlRepositoryMock.Setup(x => x.GetAll()).Returns(TestDataHelper.TestDataHelper.GetShortenedUrls().AsQueryable());

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.ShortenedUrlRepository).Returns(shortenedUrlRepositoryMock.Object);

            var existingCodes = TestDataHelper.TestDataHelper.GetShortenedUrls()
                .Select(x => x.Code);

            // Act
            UrlShorteningService urlShorteningService = new(unitOfWorkMock.Object);
            var code = await urlShorteningService.GenerateUniqueCode();

            // Assert
            Assert.IsNotNull(code);
            Assert.IsTrue(code.Length == Constants.Constants.NumberOfCharsInShortLinkCode);
            Assert.IsFalse(existingCodes.Contains(code));
        }
    }
}