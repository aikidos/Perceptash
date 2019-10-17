using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Perceptash.Transformers;

namespace Perceptash.Computers
{
    /// <summary>
    /// Реализация метода расчета 64-битной хеш-суммы.
    /// Описание алгоритма: http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public sealed class ImageDifferenceHash64Computer : IImageHashComputer<ImageDifferenceHash64>
    {
        /// <inheritdoc />
        public async Task<ImageDifferenceHash64> ComputeAsync(Stream stream, IImageTransformer transformer, 
            CancellationToken cancellationToken = default)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (transformer == null) throw new ArgumentNullException(nameof(transformer));

            byte[] pixels = await transformer.ConvertToGreyscaleAndResizeAsync(stream, 9, 8, cancellationToken);

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

            return new ImageDifferenceHash64(hash);
        }
    }
}
