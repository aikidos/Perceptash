using System.IO;
using System.Threading.Tasks;
using Perceptash.Computers;
using Perceptash.Transformers;
using Xunit;

namespace Perceptash.Tests
{
    public sealed class ImageAverageHashComputerTests
    {
        [Fact]
        public async Task Compute()
        {
            // Arrange
            IImageHashComputer<ImageAverageHash> computer = new ImageAverageHashComputer();
            IImageTransformer transformer = new ImageSixLaborsTransformer();

            await using Stream normal = File.OpenRead("test_cat.jpg");

            // Act
            ImageAverageHash hash = await computer.ComputeAsync(normal, transformer);

            // Assert
            Assert.Equal(18438820940906233866UL, hash.InternalValue);
        }
    }
}
