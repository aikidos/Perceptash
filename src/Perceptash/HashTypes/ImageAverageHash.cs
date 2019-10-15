namespace Perceptash
{
    /// <summary>
    /// Хэш-сумма, рассчитанная по алгоритму среднего хэша.
    /// </summary>
    public readonly struct ImageAverageHash : IImageHashComparable<ImageAverageHash>
    {
        /// <summary>
        /// Хэш-сумма.
        /// </summary>
        public ulong InternalValue { get; }

        /// <summary>
        /// Конструктор <see cref="ImageAverageHash"/>.
        /// </summary>
        /// <param name="value">Хэш-сумма.</param>
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
}
