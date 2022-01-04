using System.Diagnostics.Contracts;

namespace Perceptash;

/// <summary>
/// Interface used for implementation of hash comparisons.
/// </summary>
/// <typeparam name="THash">Type of hash value.</typeparam>
public interface IImageHashComparable<THash>
    where THash : struct, IImageHashComparable<THash>
{
    /// <summary>
    /// if images are the same it will return 1.0 or 0.0 for completely different images
    /// </summary>
    /// <param name="otherHash">The hash to be used for comparison.</param>
    /// <example>
    /// <code>
    ///     var hasher = new ImageHasher(transformer);
    ///  
    ///     var hash1 = hasher.Calculate("cat.jpg", KnownImageHashes.Difference64);
    ///     var hash2 = hasher.Calculate("cat_rotated_90_degrees.jpg", KnownImageHashes.Difference64);
    ///  
    ///     float similarity = hash1.Similarity(hash2); // 0.46875
    /// </code>
    /// </example>
    [Pure]
    float Similarity(THash otherHash);
}