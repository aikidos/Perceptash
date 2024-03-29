﻿using System;
using System.IO;
using Perceptash.Computers;
using Perceptash.Transformers;

namespace Perceptash;

/// <summary>
/// Implementation of the hash calculation methods.
/// </summary>
public sealed class ImageHasher : IImageHasher
{
    /// <inheritdoc />
    public IImageTransformer Transformer { get; }

    /// <summary>
    /// Initializes a new <see cref="ImageHasher"/>.
    /// </summary>
    /// <param name="transformer">Implementation of the image conversion methods.</param>
    /// <exception cref="ArgumentNullException">
    ///     The <paramref name="transformer"/> parameter is null.
    /// </exception>
    public ImageHasher(IImageTransformer transformer)
    {
        Transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
    }

    /// <inheritdoc />
    public THash Calculate<THash>(Stream stream, IImageHashComputer<THash> computer)
        where THash : struct, IImageHashComparable<THash>
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));
        if (computer == null) 
            throw new ArgumentNullException(nameof(computer));

        return computer.Compute(stream, Transformer);
    }
}