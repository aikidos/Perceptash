using System.IO;
using System.Linq;
using Moq;
using Perceptash.Computers;
using Perceptash.Transformers;
using Xunit;

namespace Perceptash.Tests
{
    public sealed class ImageDifferenceHash64ComputerTests
    {
        [Fact]
        public void Compute()
        {
            // Arrange
            IImageHashComputer<ImageDifferenceHash64> computer = new ImageDifferenceHash64Computer();

            var transformer = new Mock<IImageTransformer>();
            transformer.Setup(impl => impl.ConvertToGreyscaleAndResize(It.IsAny<Stream>(), 9, 8))
                .Returns(() => Enumerable.Range(1, 72).Reverse().Select(value => (byte) value).ToArray());

            // Act
            var hash = computer.Compute(Stream.Null, transformer.Object);

            // Assert
            Assert.Equal(18446744073709551615UL, hash.InternalValue);
        }
    }
}
