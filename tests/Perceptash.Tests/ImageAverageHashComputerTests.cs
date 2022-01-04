using System.IO;
using System.Linq;
using Moq;
using Perceptash.Computers;
using Perceptash.Transformers;
using Xunit;

namespace Perceptash.Tests;

public sealed class ImageAverageHashComputerTests
{
    [Fact]
    public void Compute()
    {
        // Arrange
        IImageHashComputer<ImageAverageHash> computer = new ImageAverageHashComputer();

        var transformer = new Mock<IImageTransformer>();
        transformer.Setup(impl => impl.ConvertToGreyscaleAndResize(It.IsAny<Stream>(), 8, 8))
            .Returns(() => Enumerable.Range(1, 64).Select(value => (byte) value).ToArray());

        // Act
        var hash = computer.Compute(Stream.Null, transformer.Object);

        // Assert
        Assert.Equal(18446744069414584320UL, hash.InternalValue);
    }
}