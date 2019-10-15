using System;
using System.IO;
using System.Threading.Tasks;
using Perceptash.Transformers;

namespace Perceptash.Computers
{
    /// <summary>
    /// Реализация метода расчета хэш-суммы.
    /// Описание алгоритма: http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public sealed class ImageDifferenceHashComputer : IImageHashComputer<ImageDifferenceHash>
    {
        /// <inheritdoc />
        public async Task<ImageDifferenceHash> ComputeAsync(Stream stream, IImageTransformer transformer)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (transformer == null) throw new ArgumentNullException(nameof(transformer));

            byte[] pixels = await transformer.ConvertToGreyscaleAndResizeAsync(stream, 9, 8);

            ulong hash = 0UL;
            int pos = 0;

            for (int y = 0; y < 8; y++)
            {
                int row = y * 9;

                for (int x = 0; x < 8; x++)
                {
                    if (pixels[row + x] > pixels[row + x + 1])
                    {
                        hash |= (1UL << pos);
                    }

                    pos++;
                }
            }

            return new ImageDifferenceHash(hash);
        }
    }
}
