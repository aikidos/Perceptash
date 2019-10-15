using System;
using System.IO;
using System.Threading.Tasks;
using Perceptash.Computers;
using Perceptash.Transformers;

namespace Perceptash
{
    /// <summary>
    /// Реализация метода расчета хэш-суммы.
    /// </summary>
    public sealed class ImageHasher : IImageHasher
    {
        /// <inheritdoc />
        public IImageTransformer Transformer { get; }

        /// <summary>
        /// Конструктор <see cref="ImageHasher"/>.
        /// </summary>
        /// <param name="transformer">Реализация методов преобразования изображения.</param>
        public ImageHasher(IImageTransformer? transformer = null)
        {
            Transformer = transformer ?? KnownImageTransformers.SixLaborsImageSharp;
        }

        /// <inheritdoc />
        public async Task<THashValue> CalculateAsync<THashValue>(Stream stream, IImageHashComputer<THashValue> computer) 
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (computer == null) throw new ArgumentNullException(nameof(computer));

            return await computer.ComputeAsync(stream, Transformer);
        }
    }
}
