using System;
using System.IO;
using System.Threading.Tasks;
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
        public static async Task<THash> CalculateAsync<THash>(this IImageHasher hasher, string filePath, IImageHashComputer<THash> computer)
            where THash : struct, IImageHashComparable<THash>
        {
            if (hasher == null) throw new ArgumentNullException(nameof(hasher));
            if (computer == null) throw new ArgumentNullException(nameof(computer));
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));

            await using var stream = File.OpenRead(filePath);

            return await hasher.CalculateAsync(stream, computer);
        }
    }
}
