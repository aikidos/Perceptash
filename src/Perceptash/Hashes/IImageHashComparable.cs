namespace Perceptash
{
    /// <summary>
    /// Интерфейс, используемый для описания реализации сравнения хэш-сумм.
    /// </summary>
    /// <typeparam name="THash">Тип значения хэш-суммы.</typeparam>
    public interface IImageHashComparable<THash>
        where THash : struct, IImageHashComparable<THash>
    {
        /// <summary>
        /// Возвращает показатель различия двух изображений исходя из хэш-суммы.
        /// Если 1.0, то изображения полностью идентичны, в то время как 0 - полностью различаются.
        /// </summary>
        /// <param name="otherHash">Хэш-сумма, которая будет использована для сравнения.</param>
        float Similarity(THash otherHash);
    }
}
