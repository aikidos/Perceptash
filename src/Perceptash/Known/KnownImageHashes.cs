using Perceptash.Computers;

namespace Perceptash
{
    /// <summary>
    /// Содержит статические экземпляры реализаций <see cref="IImageHashComputer{THash}"/> для переиспользования.
    /// </summary>
    public static class KnownImageHashes
    {
        /// <summary>
        /// Реализация расчета 64-битной хеш-суммы.
        /// Описание алгоритма: http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
        /// </summary>
        public static IImageHashComputer<ImageDifferenceHash64> Difference64 { get; } = new ImageDifferenceHash64Computer();

        /// <summary>
        /// Реализация расчета 256-битной хеш-суммы.
        /// Описание алгоритма: http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
        /// </summary>
        public static IImageHashComputer<ImageDifferenceHash256> Difference256 { get; } = new ImageDifferenceHash256Computer();

        /// <summary>
        /// Реализация расчета хеш-суммы по алгоритму среднего хеша.
        /// </summary>
        public static IImageHashComputer<ImageAverageHash> Average { get; } = new ImageAverageHashComputer();
    }
}
