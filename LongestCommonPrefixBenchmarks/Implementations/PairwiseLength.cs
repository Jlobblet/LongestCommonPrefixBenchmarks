namespace LongestCommonPrefixBenchmarks.Implementations;

public class PairwiseLength
{
    public static int LongestCommonPrefixLength(string left, string right)
    {
        var i = 0;
        for (; i < left.Length && i < right.Length; i++)
            if (left[i] != right[i])
                break;

        return i;
    }

    public static string LongestCommonPrefix(string[] strings)
    {
        var length = strings.Aggregate(int.MaxValue, (i, s) => Math.Min(i, LongestCommonPrefixLength(s, strings[0])));
        return strings[0][..length];
    }
}
