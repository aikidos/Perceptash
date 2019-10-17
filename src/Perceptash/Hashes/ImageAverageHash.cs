﻿namespace Perceptash
{
    /// <summary>
    /// Хеш-сумма, рассчитанная по алгоритму среднего хеша.
    /// </summary>
    public readonly struct ImageAverageHash : IImageHashComparable<ImageAverageHash>
    {
        /// <summary>
        /// Хеш-сумма.
        /// </summary>
        public ulong InternalValue { get; }

        /// <summary>
        /// Конструктор <see cref="ImageAverageHash"/>.
        /// </summary>
        /// <param name="value">Хеш-сумма.</param>
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
