![Actions Status](https://github.com/aikidos/Perceptash/workflows/build/badge.svg)

# Perceptash

Library offering several different perceptual hashing algorithms for detecting similar or duplicate images.

# Todo

- [ ] Set up automatic deployment of `nuget`-packages.

# API

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

|               Categories |                         Method |       Mean |   Error |  StdDev |   Allocated |
|------------------------- |------------------------------- |-----------:|--------:|--------:|------------:|
| Perceptash (ImageSharp)  | KnownImageHashes.Difference64  |   392.6 ms | 2.21 ms | 2.07 ms |    49.52 KB |
| Perceptash (ImageSharp)  | KnownImageHashes.Difference256 |   394.4 ms | 1.24 ms | 1.10 ms |     40.8 KB |
| Perceptash (ImageSharp)  | KnownImageHashes.Average       |   390.7 ms | 1.98 ms | 1.85 ms |    49.44 KB |
|                          |                                |            |         |         |             |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference64  | 1,883.1 ms | 7.97 ms | 7.07 ms |    13.13 KB |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference256 | 1,885.7 ms | 3.23 ms | 3.02 ms |    16.71 KB |
| Perceptash (Magick.NET)  | KnownImageHashes.Average       | 1,881.2 ms | 3.99 ms | 3.73 ms |    14.41 KB |
|                          |                                |            |         |         |             |
| DupImageLib (Magick.NET) | CalculateDifferenceHash64      | 1,887.2 ms | 5.37 ms | 4.76 ms |       13 KB |
| DupImageLib (Magick.NET) | CalculateDifferenceHash256     | 1,888.7 ms | 4.72 ms | 4.18 ms |    13.32 KB |
| DupImageLib (Magick.NET) | CalculateAverageHash64         | 1,884.5 ms | 6.32 ms | 5.91 ms |    13.11 KB |
|                          |                                |            |         |         |             |
| Shipwreck.Phash          | ComputeDctHash                 |   695.0 ms | 1.88 ms | 1.66 ms | 56371.38 KB |
| Shipwreck.Phash          | ComputeDigest                  |   719.8 ms | 3.33 ms | 2.95 ms | 58464.88 KB |

Links to the tested libraries:

* [DupImageLib](https://github.com/Quickshot/DupImageLib)
* [Shipwreck.Phash](https://github.com/pgrho/phash)
