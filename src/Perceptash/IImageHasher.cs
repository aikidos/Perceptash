using System.IO;
using Perceptash.Computers;
using Perceptash.Transformers;

namespace Perceptash
{
    /// <summary>
    /// Интерфейс, используемый для описания реализации метода расчета хеш-суммы.
    /// </summary>
    public interface IImageHasher
    {
        /// <summary>
        /// Реализация методов преобразования изображения.
        /// </summary>
        IImageTransformer Transformer { get; }

        /// <summary>
        /// Возвращает хеш-сумму изображения.
        /// </summary>
        /// <typeparam name="THash">Тип значения хеш-суммы.</typeparam>
        /// <param name="stream">Поток, который относится к изображению.</param>
        /// <param name="computer">Реализация метода расчета хеш-суммы.</param>
        THash Calculate<THash>(Stream stream, IImageHashComputer<THash> computer)
            where THash : struct, IImageHashComparable<THash>;
    }
}
