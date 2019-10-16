using System;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Perceptash.Transformers
{
    /// <summary>
    /// Реализация методов преобразования изображений на основе библиотеки https://github.com/SixLabors/ImageSharp
    /// </summary>
    public sealed class ImageSixLaborsTransformer : IImageTransformer
    {
        /// <inheritdoc />
        public Task<byte[]> ConvertToGreyscaleAndResizeAsync(Stream stream, int width, int height)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));

            return Task.Run(() =>
            {
                using var image = Image.Load<Gray8>(stream);

                image.Mutate(context =>
                {
                    context.Resize(width, height);
                });

                var span = image.GetPixelSpan();
                
                var pixels = new byte[span.Length];

                for (int i = 0; i < span.Length; i++)
                {
                    pixels[i] = span[i].PackedValue;
                }

                return pixels;
            });
        }
    }
}
