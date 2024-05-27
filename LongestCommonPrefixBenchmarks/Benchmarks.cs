using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace LongestCommonPrefixBenchmarks;

[MemoryDiagnoser]
[MaxRelativeError(0.02)]
public class Benchmarks
{
    public enum WordLength
    {
        Short,
        Long
    }

    public enum WordSimilarityStrategy
    {
        NoneGuaranteed,
        NearStart,
        NearEnd,
        Identical
    }

    private const int ShortMinWordLength = 5;
    private const int ShortMaxWordLength = 20;
    private const int LongMinWordLength = 150;
    private const int LongMaxWordLength = 200;
    private const string WordParts = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private string[] _strings = null!;

    [Params(1000, Priority = 0)] public int N { get; set; }
    [ParamsAllValues(Priority = 1)] public WordLength Length { get; set; }
    [ParamsAllValues(Priority = 2)] public WordSimilarityStrategy Strategy { get; set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MinWordLength(WordLength length) => length switch
    {
        WordLength.Short => ShortMinWordLength,
        WordLength.Long => LongMinWordLength,
        _ => throw new ArgumentOutOfRangeException(nameof(length))
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MaxWordLength(WordLength length) => length switch
    {
        WordLength.Short => ShortMaxWordLength,
        WordLength.Long => LongMaxWordLength,
        _ => throw new ArgumentOutOfRangeException(nameof(length))
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int MinWordLength() => MinWordLength(Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int MaxWordLength() => MaxWordLength(Length);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GenerateRandomWord(WordLength length) =>
        length switch
        {
            WordLength.Short => GenerateRandomWord(ShortMinWordLength, ShortMaxWordLength),
            WordLength.Long => GenerateRandomWord(LongMinWordLength, LongMaxWordLength),
            _ => throw new ArgumentOutOfRangeException(nameof(length))
        };

    private static string GenerateRandomWord(int minLength, int maxLength)
    {
        var length = Random.Shared.Next(minLength, maxLength);
        var chars = Enumerable.Range(0, length).Select(_ => WordParts[Random.Shared.Next(WordParts.Length)]).ToArray();
        return new string(chars);
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _strings = GenerateStringArray(N, Strategy, Length);
    }

    public static string[] GenerateStringArray(int n, WordSimilarityStrategy strategy, WordLength length)
    {
        string[] res;
        switch (strategy)
        {
            case WordSimilarityStrategy.NoneGuaranteed:
                res = Enumerable.Range(0, n).Select(_ => GenerateRandomWord(length)).ToArray();
                break;
            case WordSimilarityStrategy.NearStart:
            case WordSimilarityStrategy.NearEnd:
                var mult = strategy == WordSimilarityStrategy.NearStart ? 0.25 : 0.75;
                var prefixLength = (int)(MaxWordLength(length) * mult);
                var prefix = GenerateRandomWord(prefixLength, prefixLength);
                var minLength = Math.Max(MinWordLength(length) - prefixLength, 0);
                var maxLength = Math.Max(Math.Max(MaxWordLength(length) - prefixLength, 0), minLength);
                res = Enumerable.Repeat(prefix, n)
                    .Select(p => p + GenerateRandomWord(minLength, maxLength))
                    .ToArray();
                break;
            case WordSimilarityStrategy.Identical:
                var word = GenerateRandomWord(length);
                res = Enumerable.Repeat(word, n).ToArray();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(strategy));
        }

        return res;
    }

    // [Benchmark]
    // public string PairwiseLength() => Implementations.PairwiseLength.LongestCommonPrefix(_strings);

    // [Benchmark]
    // public string PairwiseString() => Implementations.PairwiseString.LongestCommonPrefix(_strings);

    // [Benchmark]
    // public string SalvageFor() => Implementations.SalvageFor.LongestCommonPrefix(_strings);

    // [Benchmark]
    // public string SalvageLinq() => Implementations.SalvageLinq.LongestCommonPrefix(_strings);

    [Benchmark(Baseline = true)]
    public string ForCharByChar() => Implementations.ForCharByChar.LongestCommonPrefix(_strings);

    // [Benchmark]
    // public string Unsafe() => Implementations.Unsafe.LongestCommonPrefix(_strings);

    // [Benchmark]
    // public string MinMax() => Implementations.MinMax.LongestCommonPrefix(_strings);

    [Benchmark]
    public string ForStringByString() => Implementations.ForStringByString.LongestCommonPrefix(_strings);

    [Benchmark]
    public string VectorXor() => Implementations.VectorXor.LongestCommonPrefix(_strings);
}
