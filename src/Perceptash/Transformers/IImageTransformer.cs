using System.IO;

namespace Perceptash.Transformers
{
    /// <summary>
    /// Интерфейс, используемый для описания реализаций методов преобразования изображений.
    /// </summary>
    public interface IImageTransformer
    {
        /// <summary>
        /// Преобразовывает изображение в черно-белое, изменяет его размер и возвращает результирующий массив байт.
        /// </summary>
        /// <param name="stream">Поток, который относится к изображению.</param>
        /// <param name="width">Новая ширина изображения.</param>
        /// <param name="height">Новая высота изображения.</param>
        byte[] ConvertToGreyscaleAndResize(Stream stream, int width, int height);
    }
}
