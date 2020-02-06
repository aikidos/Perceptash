![Actions Status](https://github.com/aikidos/Perceptash/workflows/build/badge.svg)

Perceptash
===

Library offering several different perceptual hashing algorithms for detecting similar or duplicate images.  

Example
---

On platforms supporting netstandard 2.1+

```csharp
// Create transformer instance using the package `Perceptash.Transformers.ImageSharp`.
var transformer = new ImageSharpTransformer();

var hasher = new ImageHasher(transformer);

var hash1 = hasher.Calculate("cat.jpg", KnownImageHashes.Difference64);
var hash2 = hasher.Calculate("cat_rotated_90_degrees.jpg", KnownImageHashes.Difference64);

float similarity = hash1.Similarity(hash2); // 0.46875
```

Available Transformers
---

*Perceptash.Transformers.ImageSharp*  
based on https://github.com/SixLabors/ImageSharp  

*Perceptash.Transformers.Magick*  
based on https://github.com/dlemstra/Magick.NET

Benchmarks
--

Image for testing: 3000x1971 (5 mb).

|               Categories |                         Method |       Mean |   Allocated |
|------------------------- |------------------------------- |-----------:|------------:|
| Perceptash (ImageSharp)  | KnownImageHashes.Difference64  |   391.4 ms |    48.25 KB |
| Perceptash (ImageSharp)  | KnownImageHashes.Difference256 |   395.7 ms |     40.8 KB |
| Perceptash (ImageSharp)  | KnownImageHashes.Average       |   388.8 ms |    49.44 KB |
|                          |                                |            |             |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference64  | 1,872.8 ms |    13.13 KB |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference256 | 1,876.0 ms |     13.5 KB |
| Perceptash (Magick.NET)  | KnownImageHashes.Average       | 1,870.6 ms |    14.41 KB |
|                          |                                |            |             |
| DupImageLib (Magick.NET) | CalculateDifferenceHash64      | 1,879.3 ms |    34.17 KB |
| DupImageLib (Magick.NET) | CalculateDifferenceHash256     | 1,886.6 ms |    35.02 KB |
| DupImageLib (Magick.NET) | CalculateAverageHash64         | 1,872.7 ms |    34.33 KB |
|                          |                                |            |             |
| Shipwreck.Phash          | ComputeDctHash                 |   688.4 ms | 56371.38 KB |
| Shipwreck.Phash          | ComputeDigest                  |   697.7 ms |  58466.2 KB |

Links to the tested libraries:

* [DupImageLib 1.1.3](https://github.com/Quickshot/DupImageLib)
* [Shipwreck.Phash 0.5.0](https://github.com/pgrho/phash)

LICENSE
===

[MIT](LICENSE)
