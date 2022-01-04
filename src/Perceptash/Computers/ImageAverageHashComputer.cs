using System;
using System.IO;
using System.Linq;
using Perceptash.Transformers;

namespace Perceptash.Computers;

/// <summary>
/// Implementation of the hash calculation method using average algorithm.
/// </summary>
public sealed class ImageAverageHashComputer : IImageHashComputer<ImageAverageHash>
{
    private const int ImageWidth = 8;
    private const int ImageHeight = 8;
    private const int PixelCount = ImageWidth * ImageHeight;

    /// <inheritdoc />
    public ImageAverageHash Compute(Stream stream, IImageTransformer transformer)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));
        if (transformer == null)
            throw new ArgumentNullException(nameof(transformer));

        var pixels = transformer.ConvertToGreyscaleAndResize(stream, ImageWidth, ImageHeight);

        var average = pixels.Sum(pixel => pixel) / PixelCount;

        var hash = 0UL;
            
        for (var i = 0; i < PixelCount; i++)
        {
            if (pixels[i] > average)
            {
                hash |= 1UL << i;
            }
        }

        return new ImageAverageHash(hash);
    }
}