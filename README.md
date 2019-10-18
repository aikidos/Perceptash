# Perceptash

Library offering several different perceptual hashing algorithms for detecting similar or duplicate images.

# Todo

- [ ] Set up automatic deployment of `nuget`-packages.
- [ ] Translate everything into English.

# API

```csharp
// Create the transformer instance using the package `Perceptash.Transformers.ImageSharp`.
IImageTransformer transformer = new ImageSharpTransformer();

IImageHasher hasher = new ImageHasher(transformer);

await using Stream cat = File.OpenRead("cat.jpg");
await using Stream rotatedCat = File.OpenRead("cat_rotated_90_degrees.jpg");

ImageDifferenceHash64 catHash = await hasher.CalculateAsync(cat, KnownImageHashes.Difference64);
ImageDifferenceHash64 rotatedCatHash = await hasher.CalculateAsync(rotatedCat, KnownImageHashes.Difference64);

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

|               Categories |                         Method |       Mean |     Error |    StdDev |  Allocated |
|------------------------- |------------------------------- |-----------:|----------:|----------:|-----------:|
| Perceptash (ImageSharp)  | KnownImageHashes.Difference64  |   392.4 ms |  3.630 ms |  3.396 ms |      824 B |
| Perceptash (ImageSharp)  | KnownImageHashes.Difference256 |   388.6 ms |  7.171 ms |  6.708 ms |      824 B |
| Perceptash (ImageSharp)  | KnownImageHashes.Average       |   384.4 ms |  4.654 ms |  4.354 ms |      824 B |
|                          |                                |            |           |           |            |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference64  | 1,666.7 ms | 12.781 ms | 11.955 ms |      816 B |
| Perceptash (Magick.NET)  | KnownImageHashes.Difference256 | 1,667.9 ms | 11.309 ms | 10.579 ms |      816 B |
| Perceptash (Magick.NET)  | KnownImageHashes.Average       | 1,673.1 ms | 19.454 ms | 18.197 ms |      816 B |
|                          |                                |            |           |           |            |
| DupImageLib (Magick.NET) | CalculateDifferenceHash64      | 1,659.8 ms |  7.016 ms |  6.563 ms |    13360 B |
| DupImageLib (Magick.NET) | CalculateDifferenceHash256     | 1,660.4 ms | 11.535 ms | 10.789 ms |    13688 B |
| DupImageLib (Magick.NET) | CalculateAverageHash64         | 1,658.0 ms | 11.087 ms |  9.829 ms |    13472 B |
|                          |                                |            |           |           |            |
| Shipwreck.Phash          | ComputeDctHash                 |   676.9 ms |  4.382 ms |  4.099 ms | 10418904 B |
| Shipwreck.Phash          | ComputeDigest                  |   687.0 ms |  3.849 ms |  3.412 ms | 10403968 B |

Links to the tested libraries:

* [DupImageLib](https://github.com/Quickshot/DupImageLib)
* [Shipwreck.Phash](https://github.com/pgrho/phash)