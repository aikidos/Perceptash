using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick;

namespace Perceptash.Transformers
{
    /// <summary>
    /// Реализация методов преобразования изображений на основе библиотеки https://github.com/dlemstra/Magick.NET
    /// </summary>
    public sealed class ImageMagickTransformer : IImageTransformer
    {
        /// <inheritdoc />
        public Task<byte[]> ConvertToGreyscaleAndResizeAsync(Stream stream, int width, int height,
            CancellationToken cancellationToken = default)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

            return Task.Run(() =>
            {
                using var image = new MagickImage(stream);

                var settings = new QuantizeSettings
                {
                    ColorSpace = ColorSpace.Gray,
                    Colors = 256
                };

                image.Quantize(settings);

                var size = new MagickGeometry(width, height)
                {
                    IgnoreAspectRatio = true
                };

                image.Resize(size);

                using var pixels = image.GetPixels();

                return pixels.GetValues();

            }, cancellationToken);
        }
    }
}
