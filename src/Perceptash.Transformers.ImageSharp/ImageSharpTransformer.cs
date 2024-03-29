﻿using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Perceptash.Transformers;

/// <summary>
/// Implementing image conversion methods based on https://github.com/SixLabors/ImageSharp
/// </summary>
public sealed class ImageSharpTransformer : IImageTransformer
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

        using var image = Image.Load<L8>(stream);

        image.Mutate(context =>
        {
            context.Resize(width, height);
        });

        if (!image.TryGetSinglePixelSpan(out var span))
        {
            throw new ArgumentException("Multi-megapixel images are not supported.", nameof(stream));
        }

        var pixels = new byte[span.Length];

        for (var i = 0; i < span.Length; i++)
        {
            pixels[i] = span[i].PackedValue;
        }

        return pixels;
    }
}