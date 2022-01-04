using System;
using System.IO;
using Perceptash.Transformers;

namespace Perceptash.Computers;

/// <summary>
/// Implementation of the 256-bit hash calculation method using difference hash.
/// See http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
/// </summary>
public sealed class ImageDifferenceHash256Computer : IImageHashComputer<ImageDifferenceHash256>
{
    private const int ImageWidth = 17;
    private const int ImageHeight = 16;

    /// <inheritdoc />
    public ImageDifferenceHash256 Compute(Stream stream, IImageTransformer transformer)
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));
        if (transformer == null)
            throw new ArgumentNullException(nameof(transformer));

        var pixels = transformer.ConvertToGreyscaleAndResize(stream, ImageWidth, ImageHeight);

        var hash = new ulong[4];

        var pos = 0;
        var part = 0;

        for (var y = 0; y < ImageHeight; y++)
        {
            var row = y * ImageWidth;

            for (var x = 0; x < ImageHeight; x++)
            {
                if (pixels[row + x] > pixels[row + x + 1])
                {
                    hash[part] |= 1UL << pos;
                }

                if (pos == 63)
                {
                    pos = 0;
                    part++;
                }
                else
                {
                    pos++;
                }
            }
        }

        return new ImageDifferenceHash256(hash);
    }
}