namespace Perceptash
{
    /// <summary>
    /// Интерфейс, используемый для описания реализации сравнения хеш-сумм.
    /// </summary>
    /// <typeparam name="THash">Тип значения хеш-суммы.</typeparam>
    public interface IImageHashComparable<THash>
        where THash : struct, IImageHashComparable<THash>
    {
        /// <summary>
        /// Возвращает показатель различия двух изображений исходя из хеш-суммы.
        /// Если 1.0, то изображения полностью идентичны, в то время как 0 - полностью различаются.
        /// </summary>
        /// <param name="otherHash">Хеш-сумма, которая будет использована для сравнения.</param>
        float Similarity(THash otherHash);
    }
}
