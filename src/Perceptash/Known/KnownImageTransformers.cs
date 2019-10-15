using Perceptash.Transformers;

namespace Perceptash
{
    /// <summary>
    /// Содержит статические экземпляры реализаций <see cref="IImageTransformer"/> для переиспользования.
    /// </summary>
    public static class KnownImageTransformers
    {
        /// <summary>
        /// Реализация методов преобразования изображений на основе библиотеки https://github.com/SixLabors/ImageSharp
        /// </summary>
        public static IImageTransformer SixLaborsImageSharp { get; } = new ImageSixLaborsTransformer();
    }
}
