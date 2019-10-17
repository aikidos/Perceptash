# Perceptash

Библиотека перцептивного хеширования для обнаружения похожих или дублирующих изображений.

# Todo

- [ ] Настроить автоматическую сборку и деплой `nuget`-пакетов.
- [ ] Перевести всё на английский.

# API

```csharp
IImageHasher hasher = new ImageHasher();

await using Stream cat = File.OpenRead("cat.jpg");
await using Stream rotatedCat = File.OpenRead("cat_rotated_90_degrees.jpg");

ImageDifferenceHash64 catHash = await hasher.CalculateAsync(cat, KnownImageHashes.Difference64);
ImageDifferenceHash64 rotatedCatHash = await hasher.CalculateAsync(rotatedCat, KnownImageHashes.Difference64);

float similarity = catHash.Similarity(rotatedCatHash);

Console.WriteLine(similarity); // 0.46875
```

> **Примечание:**  
На данный момент, поддерживается только `.NET Core 3.0` и выше.

# Benchmarks

Для сравнения производительности с другими библиотеками было взято изображение размером 3000x1971 (5 мб).

|                                          Method |       Mean |    Error |   StdDev |     Gen 0 |     Gen 1 |     Gen 2 |  Allocated |
|------------------------------------------------ |-----------:|---------:|---------:|----------:|----------:|----------:|-----------:|
|  '[Perceptash] - KnownImageHashes.Difference64' |   390.9 ms | 2.117 ms | 1.876 ms |         - |         - |         - |      824 B |
| '[Perceptash] - KnownImageHashes.Difference256' |   393.0 ms | 4.515 ms | 4.003 ms |         - |         - |         - |      824 B |
|       '[Perceptash] - KnownImageHashes.Average' |   390.0 ms | 6.832 ms | 6.391 ms |         - |         - |         - |      824 B |
|     '[DupImageLib] - CalculateDifferenceHash64' | 1,749.1 ms | 3.718 ms | 2.903 ms |         - |         - |         - |    12472 B |
|        '[DupImageLib] - CalculateAverageHash64' | 1,746.5 ms | 3.654 ms | 3.239 ms |         - |         - |         - |    12584 B |
|            '[Shipwreck.Phash] - ComputeDctHash' |   687.6 ms | 2.536 ms | 2.372 ms | 3000.0000 | 1000.0000 | 1000.0000 | 10418904 B |
|             '[Shipwreck.Phash] - ComputeDigest' |   696.0 ms | 2.638 ms | 2.468 ms | 3000.0000 | 1000.0000 | 1000.0000 | 10403968 B |

Ссылки на библиотеки, которые указаны в тесте:

* [DupImageLib](https://github.com/Quickshot/DupImageLib)
* [Shipwreck.Phash](https://github.com/pgrho/phash)

> **Примечание:**  
Большой прирост производительности, как и уменьшение потребление памяти, в большей степени, обеспечила библиотека для обработки изображений - [SixLabors.ImageSharp](https://github.com/SixLabors/ImageSharp).  
