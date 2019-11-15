![Actions Status](https://github.com/aikidos/Perceptash/workflows/build/badge.svg)

# Perceptash

Library offering several different perceptual hashing algorithms for detecting similar or duplicate images.

# Todo

- [ ] Set up automatic deployment of `nuget`-packages.

# Usage

```csharp
// Create the transformer instance using the package `Perceptash.Transformers.ImageSharp`.
IImageTransformer transformer = new ImageSharpTransformer();

IImageHasher hasher = new ImageHasher(transformer);

using Stream cat = File.OpenRead("cat.jpg");
using Stream rotatedCat = File.OpenRead("cat_rotated_90_degrees.jpg");

ImageDifferenceHash64 catHash = hasher.Calculate(cat, KnownImageHashes.Difference64);
ImageDifferenceHash64 rotatedCatHash = hasher.Calculate(rotatedCat, KnownImageHashes.Difference64);

float similarity = catHash.Similarity(rotatedCatHash);

Console.WriteLine(similarity); // 0.46875
```

> **Note:**  
At the moment, only `.NET Core 3.0` and higher is supported.

# Available Transformers

**Perceptash.Transformers.ImageSharp**  
based on https://github.com/SixLabors/ImageSharp  

**Perceptash.Transformers.Magick**  
based on https://github.com/dlemstra/Magick.NET

# Benchmarks

Image for testing: 3000x1971 (5 mb).

|               Categories |                         Method |       Mean |    Error |   StdDev |   Allocated |
|------------------------- |------------------------------- |-----------:|---------:|---------:|------------:|
| Perceptash (ImageSharp)  | KnownImageHashes.Difference64  |   391.4 ms |  1.33 ms |  1.24 ms |    48.25 KB |
| Perceptash (ImageSharp)  | KnownImageHashes.Difference256 |   395.7 ms |  3.23 ms |  3.02 ms |     40.8 KB |
| Perceptash (ImageSharp)  | KnownImageHashes.Average       |   388.8 ms |  1.07 ms |  0.95 ms |    49.44 KB |
|                          |                                |            |          |          |             |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference64  | 1,872.8 ms |  8.88 ms |  7.87 ms |    13.13 KB |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference256 | 1,876.0 ms | 11.30 ms | 10.57 ms |     13.5 KB |
| Perceptash (Magick.NET)  | KnownImageHashes.Average       | 1,870.6 ms |  6.76 ms |  6.33 ms |    14.41 KB |
|                          |                                |            |          |          |             |
| DupImageLib (Magick.NET) | CalculateDifferenceHash64      | 1,879.3 ms |  5.07 ms |  4.74 ms |    34.17 KB |
| DupImageLib (Magick.NET) | CalculateDifferenceHash256     | 1,886.6 ms |  5.43 ms |  4.82 ms |    35.02 KB |
| DupImageLib (Magick.NET) | CalculateAverageHash64         | 1,872.7 ms |  7.44 ms |  6.96 ms |    34.33 KB |
|                          |                                |            |          |          |             |
| Shipwreck.Phash          | ComputeDctHash                 |   688.4 ms |  1.58 ms |  1.48 ms | 56371.38 KB |
| Shipwreck.Phash          | ComputeDigest                  |   697.7 ms |  1.19 ms |  1.06 ms |  58466.2 KB |

Links to the tested libraries:

* [DupImageLib](https://github.com/Quickshot/DupImageLib)
* [Shipwreck.Phash](https://github.com/pgrho/phash)
