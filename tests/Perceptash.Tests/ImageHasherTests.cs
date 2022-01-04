using System.IO;
using System.Linq;
using Moq;
using Perceptash.Transformers;
using Xunit;

namespace Perceptash.Tests;

public class ImageHasherTests
{
    [Fact]
    public void Calculate()
    {
        // Arrange
        var transformer = new Mock<IImageTransformer>();
        transformer.Setup(impl => impl.ConvertToGreyscaleAndResize(It.IsAny<Stream>(), 9, 8))
            .Returns(() => Enumerable.Range(1, 72).Reverse().Select(value => (byte)value).ToArray());

        IImageHasher hasher = new ImageHasher(transformer.Object);

        // Act
        var hash = hasher.Calculate(Stream.Null, KnownImageHashes.Difference64);

        // Assert
        Assert.Equal(18446744073709551615UL, hash.InternalValue);
    }

    [Fact]
    public void Compare()
    {
        // Arrange
        var hash1 = new ImageDifferenceHash64(18446744073709551615UL);
        var hash2 = new ImageDifferenceHash64(18446744073709551615UL);
        var hash3 = new ImageDifferenceHash64(18446744073709550504UL);

        // Act
        var result1 = hash1.Similarity(hash2);
        var result2 = hash1.Similarity(hash3);

        // Assert
        Assert.Equal(1f, result1);
        Assert.Equal(0.90625f, result2);
    }
}