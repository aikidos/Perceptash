using System;
using System.IO;
using ImageMagick;

namespace Perceptash.Transformers
{
    /// <summary>
    /// Implementing image conversion methods based on https://github.com/dlemstra/Magick.NET
    /// </summary>
    public sealed class ImageMagickTransformer : IImageTransformer
    {
        /// <inheritdoc />
        public byte[] ConvertToGreyscaleAndResize(Stream stream, int width, int height)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (width <= 0) 
                throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height));

            using var image = new MagickImage(stream);

            var settings = new QuantizeSettings
            {
                ColorSpace = ColorSpace.Gray,
                Colors = 256
            };

            var size = new MagickGeometry(width, height)
            {
                IgnoreAspectRatio = true
            };

            image.Quantize(settings);

            image.Resize(size);

            using var pixels = image.GetPixels();

            return pixels.GetValues();
        }
    }
}
