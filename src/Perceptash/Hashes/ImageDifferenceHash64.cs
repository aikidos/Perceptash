namespace Perceptash
{
    /// <summary>
    /// 64-битная хеш-сумма, рассчитанная по алгоритму http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public readonly struct ImageDifferenceHash64 : IImageHashComparable<ImageDifferenceHash64>
    {
        /// <summary>
        /// Хеш-сумма.
        /// </summary>
        public ulong InternalValue { get; }

        /// <summary>
        /// Конструктор <see cref="ImageDifferenceHash64"/>.
        /// </summary>
        /// <param name="value">Хеш-сумма.</param>
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
