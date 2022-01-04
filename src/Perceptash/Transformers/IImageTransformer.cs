using System;
using System.IO;

namespace Perceptash.Transformers;

/// <summary>
/// Interface used for implementing image conversion methods.
/// </summary>
public interface IImageTransformer
{
    /// <summary>
    /// Converts an image to grayscale, resizes it and returns resulting array of bytes.
    /// </summary>
    /// <param name="stream">Stream to the image.</param>
    /// <param name="width">New image width.</param>
    /// <param name="height">New image height.</param>
    /// <exception cref="ArgumentNullException">
    ///     The <paramref name="stream"/> parameter is null.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     The <paramref name="width"/> parameter is less than or equal to zero.
    ///     The <paramref name="height"/> parameter is less than or equal to zero.
    /// </exception>
    byte[] ConvertToGreyscaleAndResize(Stream stream, int width, int height);
}