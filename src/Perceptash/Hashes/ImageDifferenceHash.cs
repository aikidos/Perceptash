namespace Perceptash
{
    /// <summary>
    /// Хэш-сумма, рассчитанная по алгоритму http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public readonly struct ImageDifferenceHash : IImageHashComparable<ImageDifferenceHash>
    {
        /// <summary>
        /// Хэш-сумма.
        /// </summary>
        public ulong InternalValue { get; }

        /// <summary>
        /// Конструктор <see cref="ImageDifferenceHash"/>.
        /// </summary>
        /// <param name="value">Хэш-сумма.</param>
        public ImageDifferenceHash(ulong value)
        {
            InternalValue = value;
        }

        /// <inheritdoc />
        public float Similarity(ImageDifferenceHash otherHash)
        {
            return HammingWeight.CalculateSimilarity(InternalValue, otherHash.InternalValue);
        }
    }
}
