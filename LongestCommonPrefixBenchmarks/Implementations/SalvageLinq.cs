namespace LongestCommonPrefixBenchmarks.Implementations;

public static class SalvageLinq
{
    public static string LongestCommonPrefix(string[] strings)
    {
        return string.Join("", strings[0]
            .TakeWhile((ch, i) => strings.All(x => x.Length > i && x[i] == ch)));
    }
}
