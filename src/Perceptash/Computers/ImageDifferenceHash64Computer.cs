using System;
using System.IO;
using Perceptash.Transformers;

namespace Perceptash.Computers
{
    /// <summary>
    /// Implementation of the 64-bit hash calculation method using difference hash.
    /// See http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public sealed class ImageDifferenceHash64Computer : IImageHashComputer<ImageDifferenceHash64>
    {
        /// <inheritdoc />
        public ImageDifferenceHash64 Compute(Stream stream, IImageTransformer transformer)
        {
            if (stream == null) 
                throw new ArgumentNullException(nameof(stream));
            if (transformer == null) 
                throw new ArgumentNullException(nameof(transformer));

            var pixels = transformer.ConvertToGreyscaleAndResize(stream, 9, 8);

            var hash = 0UL;
            var pos = 0;

            for (var y = 0; y < 8; y++)
            {
                var row = y * 9;

                for (var x = 0; x < 8; x++)
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
