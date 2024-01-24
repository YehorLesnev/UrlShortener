using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Services;

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
