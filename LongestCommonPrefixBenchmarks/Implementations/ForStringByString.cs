namespace LongestCommonPrefixBenchmarks.Implementations;

public static class ForStringByString
{
    public static string LongestCommonPrefix(string[] strings)
    {
        var prefix = strings[0];
        var prefixLength = prefix.Length;
        for (var j = 1; j < strings.Length; ++j)
        {
            var i = 0;
            var maxI = Math.Min(Math.Min(prefix.Length, strings[j].Length), prefixLength);
            for (; i < maxI; i++)
                if (prefix[i] != strings[j][i])
                    break;

            if (i == 0) return "";
            prefixLength = i;
        }

        return strings[0][..prefixLength];
    }
}
