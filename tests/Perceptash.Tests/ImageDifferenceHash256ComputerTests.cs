using System.IO;
using System.Threading.Tasks;
using Perceptash.Computers;
using Perceptash.Transformers;
using Xunit;

namespace Perceptash.Tests
{
    public sealed class ImageDifferenceHash256ComputerTests
    {
        [Fact]
        public async Task Compute()
        {
            // Arrange
            IImageHashComputer<ImageDifferenceHash256> computer = new ImageDifferenceHash256Computer();
            IImageTransformer transformer = new ImageSixLaborsTransformer();

            await using Stream normal = File.OpenRead("test_cat.jpg");

            // Act
            ImageDifferenceHash256 hash = await computer.ComputeAsync(normal, transformer);
            
            ulong[] values = hash
                .GetInternalValuesSpan()
                .ToArray();

            // Assert
            Assert.Equal(new[] { 17435256907975398798UL, 15764499245621651UL, 14348193668125099385UL, 7273899141761798589UL }, values);
        }
    }
}
