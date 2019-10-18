using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
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

            var transformer = new Mock<IImageTransformer>();
            transformer.Setup(impl => impl.ConvertToGreyscaleAndResizeAsync(It.IsAny<Stream>(), 9, 8, CancellationToken.None))
                .ReturnsAsync(() => Enumerable.Range(1, 72).Reverse().Select(value => (byte) value).ToArray());

            // Act
            ImageDifferenceHash64 hash = await computer.ComputeAsync(Stream.Null, transformer.Object);

            // Assert
            Assert.Equal(18446744073709551615UL, hash.InternalValue);
        }
    }
}
