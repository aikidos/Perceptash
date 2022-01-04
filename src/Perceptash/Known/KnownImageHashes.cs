using Perceptash.Computers;

namespace Perceptash;

/// <summary>
/// Contains reusable static instances of known calculation hash algorithms.
/// </summary>
public static class KnownImageHashes
{
    /// <summary>
    /// Implementation of the 64-bit hash calculation method using difference hash.
    /// See http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public static IImageHashComputer<ImageDifferenceHash64> Difference64 { get; } = new ImageDifferenceHash64Computer();

    /// <summary>
    /// Implementation of the 256-bit hash calculation method using difference hash.
    /// See http://www.hackerfactor.com/blog/index.php?/archives/529-Kind-of-Like-That.html
    /// </summary>
    public static IImageHashComputer<ImageDifferenceHash256> Difference256 { get; } = new ImageDifferenceHash256Computer();

    /// <summary>
    /// Implementation of the hash calculation method using average algorithm.
    /// </summary>
    public static IImageHashComputer<ImageAverageHash> Average { get; } = new ImageAverageHashComputer();
}