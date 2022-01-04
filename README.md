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

var hash1 = hasher.Calculate("cat.jpg", HashComputers.Difference64);
var hash2 = hasher.Calculate("cat_rotated_90_degrees.jpg", HashComputers.Difference64);

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

|              Categories |                      Method |       Mean | Allocated |
|------------------------ |---------------------------- |-----------:|----------:|
| Perceptash (ImageSharp) | HashComputers.Difference64  |   282.0 ms |     51 KB |
| Perceptash (ImageSharp) | HashComputers.Difference256 |   286.4 ms |     43 KB |
| Perceptash (ImageSharp) | HashComputers.Average       |   284.2 ms |     53 KB |
|                         |                             |            |           |
| Perceptash (Magick.NET) | HashComputers.Difference64  | 1,616.1 ms |     13 KB |
| Perceptash (Magick.NET) | HashComputers.Difference256 | 1,622.1 ms |     14 KB |
| Perceptash (Magick.NET) | HashComputers.Average       | 1,625.0 ms |     13 KB |

LICENSE
===

[MIT](LICENSE)
