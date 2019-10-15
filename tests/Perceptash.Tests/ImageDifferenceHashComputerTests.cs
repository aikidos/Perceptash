using System.IO;
using System.Threading.Tasks;
using Perceptash.Computers;
using Perceptash.Transformers;
using Xunit;

namespace Perceptash.Tests
{
    public sealed class ImageDifferenceHashComputerTests
    {
        [Fact]
        public async Task Compute()
        {
            // Arrange
            IImageHashComputer<ImageDifferenceHash> computer = new ImageDifferenceHashComputer();
            IImageTransformer transformer = new ImageSixLaborsTransformer();

            await using Stream normal = File.OpenRead("test_cat.jpg");

            // Act
            ImageDifferenceHash hash = await computer.ComputeAsync(normal, transformer);

            // Assert
            Assert.Equal(8000419368315833978UL, hash.InternalValue);
        }
    }
}
