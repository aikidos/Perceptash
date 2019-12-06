using System;

namespace Perceptash
{
    /// <summary>
    /// The 256-bit hash, calculated using http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public readonly struct ImageDifferenceHash256 : IImageHashComparable<ImageDifferenceHash256>
    {
        private readonly ulong[] _internalValues;

        /// <summary>
        /// Initializes a new <see cref="ImageDifferenceHash256"/>.
        /// </summary>
        /// <param name="values">Hash values.</param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="values"/> parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The length of the <paramref name="values"/> is not four.
        /// </exception>
        public ImageDifferenceHash256(ReadOnlySpan<ulong> values)
        {
            if (values == null) 
                throw new ArgumentNullException(nameof(values));
            if (values.Length != 4) 
                throw new ArgumentException("The length of the array should be four.", nameof(values));
            
            _internalValues = values.ToArray();
        }

        /// <summary>
        /// Returns hash as a span of values.
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
