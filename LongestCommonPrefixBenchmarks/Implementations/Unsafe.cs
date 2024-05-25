namespace LongestCommonPrefixBenchmarks.Implementations;

public static class Unsafe
{
    public static unsafe string LongestCommonPrefix(string[] strings)
    {
        fixed (char* p0 = strings[0])
        {
            var length = strings[0].Length;
            for (var i = 0; i < length; i++)
            {
                var c = p0[i];
                for (var j = 1; j < strings.Length; j++)
                    if (i >= strings[j].Length || strings[j][i] != c)
                        return strings[0][..i];
            }

            return strings[0];
        }
    }
}
