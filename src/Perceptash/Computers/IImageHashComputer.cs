using System.IO;
using System.Threading.Tasks;
using Perceptash.Transformers;

namespace Perceptash.Computers
{
    /// <summary>
    /// Интерфейс, используемый для описания реализации метода расчета хэш-суммы.
    /// </summary>
    /// <typeparam name="THash">Тип значения хэш-суммы.</typeparam>
    public interface IImageHashComputer<THash>
    {
        /// <summary>
        /// Возвращает хэш-сумму изображения.
        /// </summary>
        /// <param name="stream">Поток, который относится к изображению.</param>
        /// <param name="transformer">Реализация методов преобразования изображения.</param>
        Task<THash> ComputeAsync(Stream stream, IImageTransformer transformer);
    }
}
