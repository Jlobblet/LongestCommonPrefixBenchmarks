namespace LongestCommonPrefixBenchmarks.Implementations;

public static class ForCharByChar
{
    public static string LongestCommonPrefix(string[] strings)
    {
        var i = 0;
        for (; i < strings[0].Length; i++)
        for (var j = 1; j < strings.Length; j++)
            if (i >= strings[j].Length || strings[j][i] != strings[0][i])
                goto result;

        result:
        return strings[0][..i];
    }
}
