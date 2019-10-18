using System.IO;
using Perceptash.Transformers;

namespace Perceptash.Computers
{
    /// <summary>
    /// Интерфейс, используемый для описания реализации метода расчета хеш-суммы.
    /// </summary>
    /// <typeparam name="THash">Тип значения хеш-суммы.</typeparam>
    public interface IImageHashComputer<THash>
        where THash : struct, IImageHashComparable<THash>
    {
        /// <summary>
        /// Возвращает хеш-сумму изображения.
        /// </summary>
        /// <param name="stream">Поток, который относится к изображению.</param>
        /// <param name="transformer">Реализация методов преобразования изображения.</param>
        THash Compute(Stream stream, IImageTransformer transformer);
    }
}
