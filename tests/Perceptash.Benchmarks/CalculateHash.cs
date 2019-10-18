using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using DupImageLib;
using Perceptash.Transformers;
using Shipwreck.Phash;
using Shipwreck.Phash.Bitmaps;
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
        private readonly ImageHashes _dupImageLib = new ImageHashes();

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
        public async Task<ImageDifferenceHash64> Perceptash_ImageSharp_Difference_64()
        {
            return await _perceptashImageSharp.CalculateAsync(_imageStream, KnownImageHashes.Difference64);
        }

        [BenchmarkCategory("Perceptash (ImageSharp)")]
        [Benchmark(Description = "KnownImageHashes.Difference256")]
        public async Task<ImageDifferenceHash256> Perceptash_ImageSharp_Difference_256()
        {
            return await _perceptashImageSharp.CalculateAsync(_imageStream, KnownImageHashes.Difference256);
        }

        [BenchmarkCategory("Perceptash (ImageSharp)")]
        [Benchmark(Description = "KnownImageHashes.Average")]
        public async Task<ImageAverageHash> Perceptash_ImageSharp_Average()
        {
            return await _perceptashImageSharp.CalculateAsync(_imageStream, KnownImageHashes.Average);
        }

        [BenchmarkCategory("Perceptash (Magick.NET)")]
        [Benchmark(Description = "KnownImageHashes.Difference64")]
        public async Task<ImageDifferenceHash64> Perceptash_Magick_Difference_64()
        {
            return await _perceptashMagick.CalculateAsync(_imageStream, KnownImageHashes.Difference64);
        }

        [BenchmarkCategory("Perceptash (Magick.NET)")]
        [Benchmark(Description = "KnownImageHashes.Difference256")]
        public async Task<ImageDifferenceHash256> Perceptash_Magick_Difference_256()
        {
            return await _perceptashMagick.CalculateAsync(_imageStream, KnownImageHashes.Difference256);
        }

        [BenchmarkCategory("Perceptash (Magick.NET)")]
        [Benchmark(Description = "KnownImageHashes.Average")]
        public async Task<ImageAverageHash> Perceptash_Magick_Average()
        {
            return await _perceptashMagick.CalculateAsync(_imageStream, KnownImageHashes.Average);
        }

        [BenchmarkCategory("DupImageLib (Magick.NET)")]
        [Benchmark(Description = "CalculateDifferenceHash64")]
        public ulong DupImageLib_Magick_Difference_64()
        {
            return _dupImageLib.CalculateDifferenceHash64(_imageStream);
        }

        [BenchmarkCategory("DupImageLib (Magick.NET)")]
        [Benchmark(Description = "CalculateDifferenceHash256")]
        public ulong[] DupImageLib_Magick_Difference_256()
        {
            return _dupImageLib.CalculateDifferenceHash256(_imageStream);
        }

        [BenchmarkCategory("DupImageLib (Magick.NET)")]
        [Benchmark(Description = "CalculateAverageHash64")]
        public ulong DupImageLib_Magick_Average_64()
        {
            return _dupImageLib.CalculateAverageHash64(_imageStream);
        }

        [BenchmarkCategory("Shipwreck.Phash")]
        [Benchmark(Description = "ComputeDctHash")]
        public ulong PHash_Dct()
        {
            using var image = Image.FromStream(_imageStream);

            var luminanceImage = ((Bitmap) image).ToLuminanceImage();

            return ImagePhash.ComputeDctHash(luminanceImage);
        }

        [BenchmarkCategory("Shipwreck.Phash")]
        [Benchmark(Description = "ComputeDigest")]
        public Digest PHash_Digest()
        {
            using var image = Image.FromStream(_imageStream);

            var luminanceImage = ((Bitmap)image).ToLuminanceImage();

            return ImagePhash.ComputeDigest(luminanceImage);
        }
    }
}
