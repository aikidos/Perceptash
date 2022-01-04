using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Perceptash.Transformers;
using ImageMagickTransformer = Perceptash.Transformers.ImageMagickTransformer;

namespace Perceptash.Benchmarks
{
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [CategoriesColumn]
    [MemoryDiagnoser]
    public class CalculateHash
    {
        private readonly IImageHasher _perceptashImageSharp = new ImageHasher(new ImageSharpTransformer());
        private readonly IImageHasher _perceptashMagick = new ImageHasher(new ImageMagickTransformer());

        private byte[] _imageBytes;
        private MemoryStream _imageStream;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _imageBytes = File.ReadAllBytes("benchmark_image.jpg");
        }

        [IterationSetup]
        public void IterationSetup()
        {
            _imageStream = new MemoryStream(_imageBytes);
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            _imageStream.Dispose();
        }

        [BenchmarkCategory("Perceptash (ImageSharp)")]
        [Benchmark(Description = "KnownImageHashes.Difference64")]
        public ImageDifferenceHash64 Perceptash_ImageSharp_Difference_64()
        {
            return _perceptashImageSharp.Calculate(_imageStream, KnownImageHashes.Difference64);
        }

        [BenchmarkCategory("Perceptash (ImageSharp)")]
        [Benchmark(Description = "KnownImageHashes.Difference256")]
        public ImageDifferenceHash256 Perceptash_ImageSharp_Difference_256()
        {
            return _perceptashImageSharp.Calculate(_imageStream, KnownImageHashes.Difference256);
        }

        [BenchmarkCategory("Perceptash (ImageSharp)")]
        [Benchmark(Description = "KnownImageHashes.Average")]
        public ImageAverageHash Perceptash_ImageSharp_Average()
        {
            return _perceptashImageSharp.Calculate(_imageStream, KnownImageHashes.Average);
        }

        [BenchmarkCategory("Perceptash (Magick.NET)")]
        [Benchmark(Description = "KnownImageHashes.Difference64")]
        public ImageDifferenceHash64 Perceptash_Magick_Difference_64()
        {
            return _perceptashMagick.Calculate(_imageStream, KnownImageHashes.Difference64);
        }

        [BenchmarkCategory("Perceptash (Magick.NET)")]
        [Benchmark(Description = "KnownImageHashes.Difference256")]
        public ImageDifferenceHash256 Perceptash_Magick_Difference_256()
        {
            return _perceptashMagick.Calculate(_imageStream, KnownImageHashes.Difference256);
        }

        [BenchmarkCategory("Perceptash (Magick.NET)")]
        [Benchmark(Description = "KnownImageHashes.Average")]
        public ImageAverageHash Perceptash_Magick_Average()
        {
            return _perceptashMagick.Calculate(_imageStream, KnownImageHashes.Average);
        }
    }
}
