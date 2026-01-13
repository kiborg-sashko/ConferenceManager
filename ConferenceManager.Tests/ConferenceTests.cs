using Xunit;
using ConferenceManager.Core.Entities;
using System;

namespace ConferenceManager.Tests
{
    public class ConferenceTests
    {
        [Fact]
        public void IsUpcoming_ShouldReturnTrue_WhenDateIsInFuture()
        {
            // Arrange (Підготовка)
            var conference = new Conference
            {
                Name = "Future Tech",
                Date = DateTime.Now.AddDays(10) // Дата через 10 днів
            };

            // Act (Дія)
            var result = conference.IsUpcoming();

            // Assert (Перевірка)
            Assert.True(result);
        }

        [Fact]
        public void IsUpcoming_ShouldReturnFalse_WhenDateIsInPast()
        {
            // Arrange
            var conference = new Conference
            {
                Name = "Old Tech",
                Date = DateTime.Now.AddDays(-10) // Дата 10 днів тому
            };

            // Act
            var result = conference.IsUpcoming();

            // Assert
            Assert.False(result);
        }
    }
}