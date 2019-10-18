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
    public sealed class ImageAverageHashComputerTests
    {
        [Fact]
        public async Task Compute()
        {
            // Arrange
            IImageHashComputer<ImageAverageHash> computer = new ImageAverageHashComputer();

            var transformer = new Mock<IImageTransformer>();
            transformer.Setup(impl => impl.ConvertToGreyscaleAndResizeAsync(It.IsAny<Stream>(), 8, 8, CancellationToken.None))
                .ReturnsAsync(() => Enumerable.Range(1, 64).Select(value => (byte) value).ToArray());

            // Act
            ImageAverageHash hash = await computer.ComputeAsync(Stream.Null, transformer.Object);

            // Assert
            Assert.Equal(18446744069414584320UL, hash.InternalValue);
        }
    }
}
