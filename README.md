# LongestCommonPrefixBenchmarks

A collection of implementations of the longest common prefix function in C#, benchmarked against each other because I got nerdsniped.

## Rules

1. The function must have the following signature: `public static string LongestCommonPrefix(string[] strings)`.
2. The function must return the longest common prefix of the input strings.
3. The function must not mutate the input array.
4. The function may assume that the input array is not null and contains at least one element.
5. The function may assume that none of the input strings are null.
6. The function may assume that the input strings are already normalised (i.e., Unicode normalisation is not required).
7. The function must not rely on a limited character set (e.g., ASCII)&mdash;it must support any valid C# string.

## Running

To run the benchmarks, execute the following commands in the root directory of the repository:

```bash
dotnet test
dotnet run -c Release -p LongestCommonPrefixBenchmarks/LongestCommonPrefixBenchmarks.csproj
```

It is recommended to run tests first to ensure that the implementations are correct before running the benchmarks.
