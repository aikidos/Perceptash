namespace Perceptash;

/// <summary>
/// The hash, calculated using the average hash algorithm.
/// </summary>
public readonly struct ImageAverageHash : IImageHashComparable<ImageAverageHash>
{
    /// <summary>
    /// Gets the hash value.
    /// </summary>
    public ulong InternalValue { get; }

    /// <summary>
    /// Initializes a new <see cref="ImageAverageHash"/>.
    /// </summary>
    /// <param name="value">Hash value.</param>
    public ImageAverageHash(ulong value)
    {
        InternalValue = value;
    }

    /// <inheritdoc />
    public float Similarity(ImageAverageHash otherHash)
    {
        return HammingWeight.CalculateSimilarity(InternalValue, otherHash.InternalValue);
    }
}