namespace Perceptash
{
    /// <summary>
    /// The 64-bit hash, calculated using http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public readonly struct ImageDifferenceHash64 : IImageHashComparable<ImageDifferenceHash64>
    {
        /// <summary>
        /// Gets the hash value.
        /// </summary>
        public ulong InternalValue { get; }

        /// <summary>
        /// Initializes a new <see cref="ImageDifferenceHash64"/>.
        /// </summary>
        /// <param name="value">Hash value.</param>
        public ImageDifferenceHash64(ulong value)
        {
            InternalValue = value;
        }

        /// <inheritdoc />
        public float Similarity(ImageDifferenceHash64 otherHash)
        {
            return HammingWeight.CalculateSimilarity(InternalValue, otherHash.InternalValue);
        }
    }
}
