﻿using System.Numerics;

namespace LongestCommonPrefixBenchmarks.Implementations;

public class VectorXor
{
    public static unsafe int VectorPrefix(string left, string right, int maxLength = int.MaxValue)
    {
        maxLength = Math.Min(maxLength, Math.Min(left.Length, right.Length));
        fixed (char* leftPtr = left, rightPtr = right)
        {
            var offset = 0;
            var buf = stackalloc ushort[Vector<ushort>.Count];
            var span = new Span<ushort>(buf, Vector<ushort>.Count);
            for (; offset + Vector<ushort>.Count <= maxLength; offset += Vector<ushort>.Count)
            {
                var leftVector = Vector.Load((ushort*)leftPtr + offset);
                var rightVector = Vector.Load((ushort*)rightPtr + offset);
                var xor = leftVector ^ rightVector;
                if (Vector.Sum(xor) == 0) continue;

                xor.Store(buf);
                var inc = span.IndexOfAnyExcept((ushort)0);
                return offset + inc;
            }

            for (; offset < maxLength; offset++)
                if (leftPtr[offset] != rightPtr[offset])
                    return offset;

            return offset;
        }
    }

    public static string LongestCommonPrefix(string[] strings)
    {
        if (strings.Length == 0) return "";
        if (strings.Length == 1) return strings[0];
        var prefix = strings[0];
        var length = VectorPrefix(strings[0], strings[1]);
        for (var j = 2; j < strings.Length; j++)
        {
            length = Math.Min(length, VectorPrefix(strings[0], strings[j], length));
            if (length == 0) return "";
        }

        return prefix[..length];
    }
}