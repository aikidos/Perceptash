using System;

namespace Perceptash
{
    /// <summary>
    /// 256-битная хеш-сумма, рассчитанная по алгоритму http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public readonly struct ImageDifferenceHash256 : IImageHashComparable<ImageDifferenceHash256>
    {
        private readonly ulong[] _internalValues;

        /// <summary>
        /// Конструктор <see cref="ImageDifferenceHash256"/>.
        /// </summary>
        /// <param name="values">Хеш-сумма.</param>
        public ImageDifferenceHash256(ReadOnlySpan<ulong> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Length != 4) 
                throw new ArgumentException("The length of the array should be four.", nameof(values));
            
            _internalValues = values.ToArray();
        }

        /// <summary>
        /// Возвращает хеш-сумму в виде массива значений.
        /// </summary>
        public ReadOnlySpan<ulong> GetInternalValuesSpan()
        {
            return _internalValues.AsSpan();
        }

        /// <inheritdoc />
        public float Similarity(ImageDifferenceHash256 otherHash)
        {
            return HammingWeight.CalculateSimilarity(_internalValues.AsSpan(), otherHash.GetInternalValuesSpan());
        }
    }
}
