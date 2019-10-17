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

            await using Stream normal = File.OpenRead("cat.jpg");

            // Act
            ImageDifferenceHash64 hash = await hasher.CalculateAsync(normal, KnownImageHashes.Difference64);

            // Assert
            Assert.True(hash.InternalValue > 0);
        }

        [Fact]
        public async Task Compare()
        {
            // Arrange
            IImageHasher hasher = new ImageHasher();

            await using Stream normal = File.OpenRead("cat.jpg");
            await using Stream duplicate = File.OpenRead("cat.jpg");
            await using Stream rotated = File.OpenRead("cat_rotated_90_degrees.jpg");

            // Act
            ImageDifferenceHash64 normalHash = await hasher.CalculateAsync(normal, KnownImageHashes.Difference64);
            ImageDifferenceHash64 duplicateHash = await hasher.CalculateAsync(duplicate, KnownImageHashes.Difference64);
            ImageDifferenceHash64 rotatedHash = await hasher.CalculateAsync(rotated, KnownImageHashes.Difference64);

            float similarityNormalDuplicate = normalHash.Similarity(duplicateHash);
            float similarityNormalRotated = normalHash.Similarity(rotatedHash);

            // Assert
            Assert.Equal(1f, similarityNormalDuplicate);

            Assert.True(similarityNormalRotated > 0);
            Assert.NotEqual(1f, similarityNormalRotated);
        }
    }
}
