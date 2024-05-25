namespace LongestCommonPrefixBenchmarks.Implementations;

public static class PairwiseString
{
    public static string LongestCommonPrefix(string left, string right)
    {
        var i = 0;
        for (; i < left.Length && i < right.Length; i++)
            if (left[i] != right[i])
                break;
        return left[..i];
    }

    public static string LongestCommonPrefix(string[] strings)
    {
        return strings.Aggregate(LongestCommonPrefix);
    }
}
