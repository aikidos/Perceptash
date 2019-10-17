using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using DupImageLib;
using Perceptash.Transformers;
using Shipwreck.Phash;
using Shipwreck.Phash.Bitmaps;

namespace Perceptash.Benchmarks
{
    [MemoryDiagnoser]
    public class CalculateHash
    {
        private readonly IImageHasher _perceptash = new ImageHasher(new ImageSixLaborsTransformer());
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

        [Benchmark(Description = "[Perceptash] - KnownImageHashes.Difference64")]
        public async Task<ImageDifferenceHash64> Perceptash_Difference_64()
        {
            return await _perceptash.CalculateAsync(_imageStream, KnownImageHashes.Difference64);
        }

        [Benchmark(Description = "[Perceptash] - KnownImageHashes.Difference256")]
        public async Task<ImageDifferenceHash256> Perceptash_Difference_256()
        {
            return await _perceptash.CalculateAsync(_imageStream, KnownImageHashes.Difference256);
        }

        [Benchmark(Description = "[Perceptash] - KnownImageHashes.Average")]
        public async Task<ImageAverageHash> Perceptash_Average()
        {
            return await _perceptash.CalculateAsync(_imageStream, KnownImageHashes.Average);
        }

        [Benchmark(Description = "[DupImageLib] - CalculateDifferenceHash64")]
        public ulong DupImageLib_Difference_64()
        {
            return _dupImageLib.CalculateDifferenceHash64(_imageStream);
        }

        [Benchmark(Description = "[DupImageLib] - CalculateAverageHash64")]
        public ulong DupImageLib_Average_64()
        {
            return _dupImageLib.CalculateAverageHash64(_imageStream);
        }

        [Benchmark(Description = "[Shipwreck.Phash] - ComputeDctHash")]
        public ulong PHash_Dct()
        {
            using var image = Image.FromStream(_imageStream);

            var luminanceImage = ((Bitmap) image).ToLuminanceImage();

            return ImagePhash.ComputeDctHash(luminanceImage);
        }

        [Benchmark(Description = "[Shipwreck.Phash] - ComputeDigest")]
        public Digest PHash_Digest()
        {
            using var image = Image.FromStream(_imageStream);

            var luminanceImage = ((Bitmap)image).ToLuminanceImage();

            return ImagePhash.ComputeDigest(luminanceImage);
        }
    }
}
