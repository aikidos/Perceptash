using System.IO;
using System.Linq;
using Moq;
using Perceptash.Computers;
using Perceptash.Transformers;
using Xunit;

namespace Perceptash.Tests
{
    public sealed class ImageDifferenceHash256ComputerTests
    {
        [Fact]
        public void Compute()
        {
            // Arrange
            IImageHashComputer<ImageDifferenceHash256> computer = new ImageDifferenceHash256Computer();

            var transformer = new Mock<IImageTransformer>();
            transformer.Setup(impl => impl.ConvertToGreyscaleAndResize(It.IsAny<Stream>(), 17, 16))
                .Returns(() => Enumerable.Range(1, 272).Reverse().Select(value => (byte) value).ToArray());

            // Act
            var hash = computer.Compute(Stream.Null, transformer.Object);
            
            var values = hash
                .GetInternalValuesSpan()
                .ToArray();

            // Assert
            Assert.Equal(Enumerable.Repeat(18446744073709551615UL, 4), values);
        }
    }
}
