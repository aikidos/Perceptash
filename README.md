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

|               Categories |                         Method |       Mean |     Error |    StdDev |   Allocated |
|------------------------- |------------------------------- |-----------:|----------:|----------:|------------:|
| Perceptash (ImageSharp)  | KnownImageHashes.Difference64  |   387.2 ms |  2.908 ms |  2.720 ms |    48.25 KB |
| Perceptash (ImageSharp)  | KnownImageHashes.Difference256 |   389.4 ms |  2.620 ms |  2.451 ms |     40.8 KB |
| Perceptash (ImageSharp)  | KnownImageHashes.Average       |   390.2 ms |  4.986 ms |  4.664 ms |    49.44 KB |
|                          |                                |            |           |           |             |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference64  | 1,669.2 ms | 16.199 ms | 14.360 ms |    13.17 KB |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference256 | 1,668.3 ms | 10.973 ms | 10.264 ms |    13.55 KB |
| Perceptash (Magick.NET)  | KnownImageHashes.Average       | 1,667.9 ms | 19.372 ms | 18.121 ms |     13.2 KB |
|                          |                                |            |           |           |             |
| DupImageLib (Magick.NET) | CalculateDifferenceHash64      | 1,682.0 ms | 20.820 ms | 19.475 ms |    13.05 KB |
| DupImageLib (Magick.NET) | CalculateDifferenceHash256     | 1,688.1 ms | 36.941 ms | 39.526 ms |    13.37 KB |
| DupImageLib (Magick.NET) | CalculateAverageHash64         | 1,671.3 ms | 12.579 ms | 11.766 ms |    13.16 KB |
|                          |                                |            |           |           |             |
| Shipwreck.Phash          | ComputeDctHash                 |   696.0 ms |  1.984 ms |  1.759 ms | 10174.71 KB |
| Shipwreck.Phash          | ComputeDigest                  |   692.0 ms |  4.319 ms |  4.040 ms | 10160.13 KB |

Links to the tested libraries:

* [DupImageLib](https://github.com/Quickshot/DupImageLib)
* [Shipwreck.Phash](https://github.com/pgrho/phash)