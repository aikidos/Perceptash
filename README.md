# Perceptash

Библиотека перцептивного хеширования для обнаружения похожих или дублирующих изображений.

# API

```csharp
IImageHasher hasher = new ImageHasher();

await using Stream cat = File.OpenRead("cat.jpg");
await using Stream rotatedCat = File.OpenRead("cat_rotated_90_degrees.jpg");

ImageDifferenceHash catHash = await hasher.CalculateAsync(cat, KnownImageHashes.Difference);
ImageDifferenceHash rotatedCatHash = await hasher.CalculateAsync(rotatedCat, KnownImageHashes.Difference);

float similarity = catHash.Similarity(rotatedCatHash);

Console.WriteLine(similarity); // 0.46875
```

# Benchmarks

Для сравнения производительности с другими библиотеками было взято изображение размером 3000x1971.

|                                       Method |       Mean |      Error |     StdDev |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|--------------------------------------------- |-----------:|-----------:|-----------:|----------:|----------:|----------:|-----------:|
| '[Perceptash] - KnownImageHashes.Difference' |   389.8 ms |  0.7583 ms |  0.6722 ms |         - |         - |         - |      792 B |
|    '[Perceptash] - KnownImageHashes.Average' |   393.2 ms |  2.0445 ms |  1.9124 ms |         - |         - |         - |      792 B |
|  '[DupImageLib] - CalculateDifferenceHash64' | 1,775.7 ms | 11.5203 ms | 10.7761 ms |         - |         - |         - |    12472 B |
|     '[DupImageLib] - CalculateAverageHash64' | 1,768.7 ms |  7.7458 ms |  7.2454 ms |         - |         - |         - |    12584 B |
|         '[Shipwreck.Phash] - ComputeDctHash' |   708.7 ms |  3.8653 ms |  3.0178 ms | 3000.0000 | 1000.0000 | 1000.0000 | 10419024 B |
|          '[Shipwreck.Phash] - ComputeDigest' |   697.2 ms |  1.3077 ms |  1.0920 ms | 3000.0000 | 1000.0000 | 1000.0000 | 10403968 B |

