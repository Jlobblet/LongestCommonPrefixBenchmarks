using System.Numerics;

namespace LongestCommonPrefixBenchmarks.Implementations;

public class VectorXor
{
    public static unsafe int VectorPrefix(string left, string right, int maxLength = int.MaxValue)
    {
        maxLength = Math.Min(maxLength, Math.Min(left.Length, right.Length));
        fixed (char* leftPtr = left, rightPtr = right)
        {
            var offset = 0;
            for (; offset + Vector<ushort>.Count <= maxLength; offset += Vector<ushort>.Count)
            {
                var leftVector = Vector.Load((ushort*)leftPtr + offset);
                var rightVector = Vector.Load((ushort*)rightPtr + offset);
                if (!Vector.EqualsAll(leftVector ^ rightVector, Vector<ushort>.Zero)) break;
            }

            for (; offset < maxLength; offset++)
                if (leftPtr[offset] != rightPtr[offset])
                    break;

            return offset;
        }
    }

    public static string LongestCommonPrefix(string[] strings)
    {
        if (strings.Length == 0) return "";
        var prefix = strings[0];
        if (strings.Length == 1) return prefix;
        if (prefix.Length == 0) return "";
        for (var j = 1; j < strings.Length; j++)
            if (strings[j].Length == 0 || strings[j][0] != prefix[0])
                return "";

        var length = VectorPrefix(strings[0], strings[1]);
        for (var j = 2; j < strings.Length; j++)
        {
            length = Math.Min(length, VectorPrefix(strings[0], strings[j], length));
            if (length == 0) return "";
        }

        return prefix[..length];
    }
}
