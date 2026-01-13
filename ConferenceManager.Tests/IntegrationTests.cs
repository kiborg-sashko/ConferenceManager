using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ConferenceManager.Tests
{
    // WebApplicationFactory запускає твій сайт у пам'яті для тестів
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Swagger_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/swagger/index.html");

            // Assert
            response.EnsureSuccessStatusCode(); // Перевіряє, що код відповіді 200-299
        }

        [Fact]
        public async Task Get_ApiV1_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/conferences");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}