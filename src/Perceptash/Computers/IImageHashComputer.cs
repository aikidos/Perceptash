using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Perceptash.Transformers;

namespace Perceptash.Computers
{
    /// <summary>
    /// Интерфейс, используемый для описания реализации метода расчета хэш-суммы.
    /// </summary>
    /// <typeparam name="THash">Тип значения хэш-суммы.</typeparam>
    public interface IImageHashComputer<THash>
        where THash : struct, IImageHashComparable<THash>
    {
        /// <summary>
        /// Возвращает хэш-сумму изображения.
        /// </summary>
        /// <param name="stream">Поток, который относится к изображению.</param>
        /// <param name="transformer">Реализация методов преобразования изображения.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        Task<THash> ComputeAsync(Stream stream, IImageTransformer transformer, CancellationToken cancellationToken = default);
    }
}
