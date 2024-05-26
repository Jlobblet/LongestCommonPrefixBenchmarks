namespace LongestCommonPrefixTests;

public record ValidStrings(string[] Strings)
{
    public override string ToString()
    {
        return $"{nameof(ValidStrings)}[{string.Join(", ", Strings)}]";
    }
}
