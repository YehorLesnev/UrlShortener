using UrlShortener.Services.Static;

namespace UrlShortener.Tests
{
    [TestClass]
    public class TestUrlValidationService
    {
        [TestMethod]
        public void Test_IsUrlValid()
        {
            // Arrange
            var validUrls = TestDataHelper.TestDataHelper.GetValidUrls();
            var invalidUrls = TestDataHelper.TestDataHelper.GetInvalidUrls();

            // Act
            // Assert
            Assert.IsTrue(validUrls.All(u => UlrValidationService.IsUrlValid(u, UriKind.Absolute)));
            Assert.IsTrue(invalidUrls.All(u => !UlrValidationService.IsUrlValid(u, UriKind.Absolute)));
        }
    }
}
