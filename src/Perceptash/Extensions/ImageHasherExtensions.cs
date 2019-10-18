using System;
using System.IO;
using Perceptash.Computers;

namespace Perceptash
{
    /// <summary>
    /// Вспомогательный класс для работы с реализациями <see cref="IImageHasher"/>.
    /// </summary>
    public static class ImageHasherExtensions
    {
        /// <summary>
        /// Возвращает хеш-сумму файла изображения.
        /// </summary>
        /// <typeparam name="THash">Тип значения хеш-суммы.</typeparam>
        /// <param name="hasher">Реализация <see cref="IImageHasher"/>.</param>
        /// <param name="filePath">Полный путь до файла изображения.</param>
        /// <param name="computer">Реализация метода расчета хеш-суммы.</param>
        public static THash Calculate<THash>(this IImageHasher hasher, string filePath, IImageHashComputer<THash> computer)
            where THash : struct, IImageHashComparable<THash>
        {
            if (hasher == null) throw new ArgumentNullException(nameof(hasher));
            if (computer == null) throw new ArgumentNullException(nameof(computer));
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));

            using var stream = File.OpenRead(filePath);

            return hasher.Calculate(stream, computer);
        }
    }
}
