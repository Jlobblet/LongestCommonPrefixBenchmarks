namespace LongestCommonPrefixBenchmarks.Implementations;

public static class MinMax
{
    public static string LongestCommonPrefix(string[] strings)
    {
        string min = strings[0], max = strings[0];
        for (var i = 1; i < strings.Length; i++)
        {
            if (strings[i].CompareTo(min) < 0) min = strings[i];
            if (strings[i].CompareTo(max) > 0) max = strings[i];
        }

        return PairwiseString.LongestCommonPrefix(min, max);
    }
}
