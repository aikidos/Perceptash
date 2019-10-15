using Perceptash.Computers;

namespace Perceptash
{
    /// <summary>
    /// Содержит статические экземпляры реализаций <see cref="IImageHashComputer{THash}"/> для переиспользования.
    /// </summary>
    public static class KnownImageHashes
    {
        /// <summary>
        /// Реализация расчета хэш-суммы.
        /// Описание алгоритма: http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
        /// </summary>
        public static IImageHashComputer<ImageDifferenceHash> Difference { get; } = new ImageDifferenceHashComputer();

        /// <summary>
        /// Реализация расчета хэш-суммы по алгоритму среднего хэша.
        /// </summary>
        public static IImageHashComputer<ImageAverageHash> Average { get; } = new ImageAverageHashComputer();
    }
}
