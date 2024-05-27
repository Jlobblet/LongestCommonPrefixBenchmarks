using FsCheck.Xunit;
using LongestCommonPrefixBenchmarks.Implementations;
using Xunit;

namespace LongestCommonPrefixTests;

[Properties(Arbitrary = [typeof(ValidStringArb)])]
public class BaselineTests
{
    private static readonly Func<string[], string> Oracle = ForCharByChar.LongestCommonPrefix;

    [Property]
    public bool OracleResultIsStartOfAll(ValidStrings vs)
    {
        var prefix = Oracle(vs.Strings);
        return vs.Strings.All(s => s.StartsWith(prefix));
    }

    [Property]
    public bool OracleResultIsLongestPrefix(ValidStrings vs)
    {
        var prefix = Oracle(vs.Strings);
        var minLength = vs.Strings.Min(s => s.Length);
        return Enumerable.Range(0, minLength)
            .Where(i => i > prefix.Length)
            .Select(i => vs.Strings[0][..i])
            .All(p => !vs.Strings.All(s => s.StartsWith(p)));
    }

    [Theory]
    [InlineData(new[] { "foobar", "foobaz", "food" }, "foo")]
    [InlineData(new[] { "foo", "foobar", "foobazquz" }, "foo")]
    [InlineData(new[] { "foo", "bar", "baz" }, "")]
    public void OracleKnownSamples(string[] strings, string expected)
    {
        var actual = Oracle(strings);
        Assert.Equal(expected, actual);
    }

    [Property]
    public bool ForStringByStringSameAsOracle(ValidStrings vs)
    {
        var actual = ForStringByString.LongestCommonPrefix(vs.Strings);
        var expected = Oracle(vs.Strings);
        return actual == expected;
    }

    [Property]
    public bool MinMaxSameAsOracle(ValidStrings vs)
    {
        var actual = MinMax.LongestCommonPrefix(vs.Strings);
        var expected = Oracle(vs.Strings);
        return actual == expected;
    }

    [Property]
    public bool PairwiseLengthSameAsOracle(ValidStrings vs)
    {
        var actual = PairwiseLength.LongestCommonPrefix(vs.Strings);
        var expected = Oracle(vs.Strings);
        return actual == expected;
    }

    [Property]
    public bool PairwiseStringSameAsOracle(ValidStrings vs)
    {
        var actual = PairwiseString.LongestCommonPrefix(vs.Strings);
        var expected = Oracle(vs.Strings);
        return actual == expected;
    }

    [Property]
    public bool SalvageForSameAsOracle(ValidStrings vs)
    {
        var actual = SalvageFor.LongestCommonPrefix(vs.Strings);
        var expected = Oracle(vs.Strings);
        return actual == expected;
    }

    [Property]
    public bool SalvageLinqSameAsOracle(ValidStrings vs)
    {
        var actual = SalvageLinq.LongestCommonPrefix(vs.Strings);
        var expected = Oracle(vs.Strings);
        return actual == expected;
    }

    [Property]
    public bool UnsafeSameAsOracle(ValidStrings vs)
    {
        var actual = Unsafe.LongestCommonPrefix(vs.Strings);
        var expected = Oracle(vs.Strings);
        return actual == expected;
    }

    [Property]
    public bool VectorXorSameAsOracle(ValidStrings vs)
    {
        var actual = VectorXor.LongestCommonPrefix(vs.Strings);
        var expected = Oracle(vs.Strings);
        return actual == expected;
    }
}
