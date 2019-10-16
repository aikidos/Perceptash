using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Perceptash.Computers;
using Perceptash.Transformers;

namespace Perceptash
{
    /// <summary>
    /// Интерфейс, используемый для описания реализации метода расчета хэш-суммы.
    /// </summary>
    public interface IImageHasher
    {
        /// <summary>
        /// Реализация методов преобразования изображения.
        /// </summary>
        IImageTransformer Transformer { get; }

        /// <summary>
        /// Возвращает хэш-сумму изображения.
        /// </summary>
        /// <typeparam name="THash">Тип значения хэш-суммы.</typeparam>
        /// <param name="stream">Поток, который относится к изображению.</param>
        /// <param name="computer">Реализация метода расчета хэш-суммы.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task<THash> CalculateAsync<THash>(Stream stream, IImageHashComputer<THash> computer, CancellationToken cancellationToken = default)
            where THash : struct, IImageHashComparable<THash>;
    }
}
