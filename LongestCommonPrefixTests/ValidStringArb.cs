using FsCheck;
using FsCheck.Fluent;
using LongestCommonPrefixBenchmarks;

namespace LongestCommonPrefixTests;

public static class ValidStringArb
{
    public static Arbitrary<ValidStrings> ValidStrings =>
        Gen.Elements(Benchmarks.WordSimilarityStrategy.NoneGuaranteed, Benchmarks.WordSimilarityStrategy.NearStart,
                Benchmarks.WordSimilarityStrategy.NearEnd, Benchmarks.WordSimilarityStrategy.Identical)
            .Zip(Gen.Choose(10, 1000))
            .Select(pair =>
                new ValidStrings(Benchmarks.GenerateStringArray(pair.Item2, pair.Item1, Benchmarks.WordLength.Short)))
            .ToArbitrary();
}
