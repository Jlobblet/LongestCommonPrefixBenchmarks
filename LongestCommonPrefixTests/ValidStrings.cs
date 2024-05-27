namespace LongestCommonPrefixTests;

public record ValidStrings(string[] Strings)
{
    public override string ToString() => $"{nameof(ValidStrings)}[{string.Join(", ", Strings)}]";
}
