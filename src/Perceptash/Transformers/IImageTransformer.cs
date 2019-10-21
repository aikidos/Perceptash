using System.IO;

namespace Perceptash.Transformers
{
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
        byte[] ConvertToGreyscaleAndResize(Stream stream, int width, int height);
    }
}
