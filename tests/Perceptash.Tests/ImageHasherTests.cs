using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Perceptash.Tests
{
    public class ImageHasherTests
    {
        [Fact]
        public async Task Calculate()
        {
            // Arrange
            IImageHasher hasher = new ImageHasher();

            await using Stream normal = File.OpenRead("test_cat.jpg");

            // Act
            ImageDifferenceHash hash = await hasher.CalculateAsync(normal, KnownImageHashes.Difference);

            // Assert
            Assert.True(hash.InternalValue > 0);
        }

        [Fact]
        public async Task Compare()
        {
            // Arrange
            IImageHasher hasher = new ImageHasher();

            await using Stream normal = File.OpenRead("test_cat.jpg");
            await using Stream duplicate = File.OpenRead("test_cat.jpg");
            await using Stream rotated = File.OpenRead("test_cat_rotate.jpg");

            // Act
            ImageDifferenceHash normalHash = await hasher.CalculateAsync(normal, KnownImageHashes.Difference);
            ImageDifferenceHash duplicateHash = await hasher.CalculateAsync(duplicate, KnownImageHashes.Difference);
            ImageDifferenceHash rotatedHash = await hasher.CalculateAsync(rotated, KnownImageHashes.Difference);

            float similarityNormalDuplicate = normalHash.Similarity(duplicateHash);
            float similarityNormalRotated = normalHash.Similarity(rotatedHash);

            // Assert
            Assert.Equal(1f, similarityNormalDuplicate);

            Assert.True(similarityNormalRotated > 0);
            Assert.NotEqual(1f, similarityNormalRotated);
        }
    }
}
