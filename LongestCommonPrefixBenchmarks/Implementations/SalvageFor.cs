namespace LongestCommonPrefixBenchmarks.Implementations;

public static class SalvageFor
{
    public static string LongestCommonPrefix(string[] strings)
    {
        for (var i = 0; i < strings[0].Length; i++)
            if (strings.Any(x => x.Length <= i || x[i] != strings[0][i]))
                return strings[0][..i];

        return strings[0];
    }
}
