using System.IO;
using System.Threading.Tasks;
using Perceptash.Computers;
using Perceptash.Transformers;
using Xunit;

namespace Perceptash.Tests
{
    public sealed class ImageDifferenceHash64ComputerTests
    {
        [Fact]
        public async Task Compute()
        {
            // Arrange
            IImageHashComputer<ImageDifferenceHash64> computer = new ImageDifferenceHash64Computer();
            IImageTransformer transformer = new ImageSixLaborsTransformer();

            await using Stream normal = File.OpenRead("cat.jpg");

            // Act
            ImageDifferenceHash64 hash = await computer.ComputeAsync(normal, transformer);

            // Assert
            Assert.Equal(8000419368315833978UL, hash.InternalValue);
        }
    }
}
