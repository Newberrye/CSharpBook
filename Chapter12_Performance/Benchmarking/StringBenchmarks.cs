using BenchmarkDotNet.Attributes; // [Benchmark]

public class StringBenchmarks
{
    int[] numbers;

    /// <summary>
    /// Creates numbers to test
    /// </summary>
    public StringBenchmarks()
    {
        numbers = Enumerable.Range(1, 20).ToArray();
    }

    /// <summary>
    /// Sets string concatenation as the baseline and performs string concatentation using + operator
    /// </summary>
    /// <returns></returns>
    [Benchmark(Baseline = true)]
    public string StringConcatenationTest()
    {
        string s = string.Empty;
        for (int i = 0; i < numbers.Length; i++)
        {
            s += numbers[i] + ", ";
        }
        return s;
    }


    /// <summary>
    /// Uses StringBuilder to build string rather than concatenation.
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public string StringBuilderTest()
    {
        System.Text.StringBuilder builder = new();
        for (int i = 0; i < numbers.Length; i++)
        {
            builder.Append(numbers[i]);
            builder.Append(", ");
        }

        return builder.ToString();
    }
}
