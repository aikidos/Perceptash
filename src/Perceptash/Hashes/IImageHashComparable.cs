namespace Perceptash
{
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
        float Similarity(THash otherHash);
    }
}
