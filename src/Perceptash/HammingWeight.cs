namespace Perceptash
{
    /// <summary>
    /// https://en.wikipedia.org/wiki/Hamming_weight
    /// </summary>
    internal static class HammingWeight
    {
        private const ulong M1 = 0x5555555555555555;
        private const ulong M2 = 0x3333333333333333;
        private const ulong M4 = 0x0f0f0f0f0f0f0f0f;
        private const ulong H01 = 0x0101010101010101;

        private static ulong Calculate(ulong hash)
        {
            hash -= (hash >> 1) & M1;
            hash = (hash & M2) + ((hash >> 2) & M2);
            hash = (hash + (hash >> 4)) & M4;
            hash = (hash * H01) >> 56;

            return hash;
        }

        public static float CalculateSimilarity(ulong hash1, ulong hash2)
        {
            return 1f - Calculate(hash1 ^ hash2) / 64f;
        }
    }
}
