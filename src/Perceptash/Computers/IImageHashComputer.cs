using System;
using System.IO;
using Perceptash.Transformers;

namespace Perceptash.Computers
{
    /// <summary>
    /// Interface used for implementing calculation hash algorithms.
    /// </summary>
    /// <typeparam name="THash">Type of hash value.</typeparam>
    public interface IImageHashComputer<THash>
        where THash : struct, IImageHashComparable<THash>
    {
        /// <summary>
        /// Returns hash of the image.
        /// </summary>
        /// <param name="stream">Stream to the image.</param>
        /// <param name="transformer">Implementation of image conversion methods.</param>
        /// <exception cref="ArgumentNullException">
        ///     The <paramref name="stream"/> parameter is null.
        ///     The <paramref name="transformer"/> parameter is null.
        /// </exception>
        THash Compute(Stream stream, IImageTransformer transformer);
    }
}
