using System;
using System.IO;
using System.Linq;
using Perceptash.Transformers;

namespace Perceptash.Computers
{
    /// <summary>
    /// Implementation of the hash calculation method using average algorithm.
    /// </summary>
    public sealed class ImageAverageHashComputer : IImageHashComputer<ImageAverageHash>
    {
        /// <inheritdoc />
        public ImageAverageHash Compute(Stream stream, IImageTransformer transformer)
        {
            if (stream == null) 
                throw new ArgumentNullException(nameof(stream));
            if (transformer == null) 
                throw new ArgumentNullException(nameof(transformer));

            byte[] pixels = transformer.ConvertToGreyscaleAndResize(stream, 8, 8);

            int total = pixels.Sum(pixel => pixel);
            int average = total / 64;

            ulong hash = 0UL;
            
            for (int i = 0; i < 64; i++)
            {
                if (pixels[i] > average)
                {
                    hash |= (1UL << i);
                }
            }

            return new ImageAverageHash(hash);
        }
    }
}
