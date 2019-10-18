using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Perceptash.Computers;
using Perceptash.Transformers;

namespace Perceptash
{
    /// <summary>
    /// Реализация метода расчета хеш-суммы.
    /// </summary>
    public sealed class ImageHasher : IImageHasher
    {
        /// <inheritdoc />
        public IImageTransformer Transformer { get; }

        /// <summary>
        /// Конструктор <see cref="ImageHasher"/>.
        /// </summary>
        /// <param name="transformer">Реализация методов преобразования изображения.</param>
        public ImageHasher(IImageTransformer transformer)
        {
            Transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
        }

        /// <inheritdoc />
        public async Task<THash> CalculateAsync<THash>(Stream stream, IImageHashComputer<THash> computer, 
            CancellationToken cancellationToken = default)
            where THash : struct, IImageHashComparable<THash>
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (computer == null) throw new ArgumentNullException(nameof(computer));

            return await computer.ComputeAsync(stream, Transformer, cancellationToken);
        }
    }
}
