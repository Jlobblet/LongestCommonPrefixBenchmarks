namespace LongestCommonPrefixBenchmarks.Implementations;

public static class Sort
{
    public static string LongestCommonPrefix(string[] strings)
    {
        Array.Sort(strings);
        return PairwiseString.LongestCommonPrefix(strings[0], strings[^1]);
    }
}
