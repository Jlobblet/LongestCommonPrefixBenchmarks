using BenchmarkDotNet.Attributes;

namespace LongestCommonPrefixBenchmarks;

[MemoryDiagnoser]
[MaxRelativeError(0.005)]
public class Benchmarks
{
    public enum WordSimilarityStrategy
    {
        NoneGuaranteed,
        NearStart,
        NearEnd,
        Identical
    }

    private const int MinWordLength = 5;
    private const int MaxWordLength = 20;
    public const string WordParts = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private string[] _strings = null!;

    [ParamsAllValues] public WordSimilarityStrategy Strategy { get; set; }

    [Params(1000)] public int N { get; set; }

    public static string GenerateRandomWord(int minLength = MinWordLength, int maxLength = MaxWordLength)
    {
        var length = Random.Shared.Next(minLength, maxLength);
        var chars = Enumerable.Range(0, length).Select(_ => WordParts[Random.Shared.Next(WordParts.Length)]).ToArray();
        return new string(chars);
    }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _strings = GenerateStringArray(N, Strategy);
    }

    public static string[] GenerateStringArray(int n, WordSimilarityStrategy strategy)
    {
        string[] res;
        switch (strategy)
        {
            case WordSimilarityStrategy.NoneGuaranteed:
                res = Enumerable.Range(0, n).Select(_ => GenerateRandomWord()).ToArray();
                break;
            case WordSimilarityStrategy.NearStart:
            case WordSimilarityStrategy.NearEnd:
                var prefixLength = strategy == WordSimilarityStrategy.NearStart ? 3 : 10;
                var prefix = GenerateRandomWord(prefixLength)[..prefixLength];
                res = Enumerable.Repeat(prefix, n)
                    .Select(p => p + GenerateRandomWord(maxLength: MaxWordLength - prefixLength)).ToArray();
                break;
            case WordSimilarityStrategy.Identical:
                var word = GenerateRandomWord();
                res = Enumerable.Repeat(word, n).ToArray();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return res;
    }

    // [Benchmark]
    // public string PairwiseLength()
    // {
    //     return Implementations.PairwiseLength.LongestCommonPrefix(_strings);
    // }

    // [Benchmark]
    // public string PairwiseString()
    // {
    //     return Implementations.PairwiseString.LongestCommonPrefix(_strings);
    // }

    // [Benchmark]
    // public string SalvageFor()
    // {
    //     return Implementations.SalvageFor.LongestCommonPrefix(_strings);
    // }

    // [Benchmark]
    // public string SalvageLinq()
    // {
    //     return Implementations.SalvageLinq.LongestCommonPrefix(_strings);
    // }

    [Benchmark(Baseline = true)]
    public string ForCharByChar()
    {
        return Implementations.ForCharByChar.LongestCommonPrefix(_strings);
    }

    [Benchmark]
    public string Unsafe()
    {
        return Implementations.Unsafe.LongestCommonPrefix(_strings);
    }

    // [Benchmark]
    // public string MinMax()
    // {
    //     return Implementations.MinMax.LongestCommonPrefix(_strings);
    // }

    [Benchmark]
    public string ForStringByString()
    {
        return Implementations.ForStringByString.LongestCommonPrefix(_strings);
    }
}
