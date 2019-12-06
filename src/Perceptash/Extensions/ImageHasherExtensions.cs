using System;
using System.IO;
using Perceptash.Computers;

namespace Perceptash
{
    /// <summary>
    /// Helper class for working with implementations <see cref="IImageHasher"/>.
    /// </summary>
    public static class ImageHasherExtensions
    {
        /// <summary>
        /// Returns hash of the image.
        /// </summary>
        /// <typeparam name="THash">Type of hash value.</typeparam>
        /// <param name="hasher">Implementation of the <see cref="IImageHasher"/>.</param>
        /// <param name="filePath">Path to an image to be hashed.</param>
        /// <param name="computer">Implementation of the hash calculation algorithm.</param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="hasher"/> parameter is null.
        ///     The <paramref name="computer"/> parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The <paramref name="filePath"/> parameter is null, empty, or consists only of white-space characters.
        /// </exception>
        /// <example>
        /// <code>
        ///     var hasher = new ImageHasher(transformer);
        ///  
        ///     var hash = hasher.Calculate("cat.jpg", KnownImageHashes.Difference64);
        /// </code>
        /// </example>
        public static THash Calculate<THash>(this IImageHasher hasher, string filePath, IImageHashComputer<THash> computer)
            where THash : struct, IImageHashComparable<THash>
        {
            if (hasher == null) 
                throw new ArgumentNullException(nameof(hasher));
            if (computer == null) 
                throw new ArgumentNullException(nameof(computer));
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));

            using var stream = File.OpenRead(filePath);

            return hasher.Calculate(stream, computer);
        }
    }
}
