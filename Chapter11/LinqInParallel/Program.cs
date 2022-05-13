using System.Diagnostics; // Stopwatch

using static System.Console;

Stopwatch watch = new();
Write("Press ENTER to Start. ");
ReadLine();
watch.Start();

int max = 45;

IEnumerable<int> numbers = Enumerable.Range(1, max);

WriteLine($"Calculating Fibonacci sequence up to {max}. Please wait...");

int[] fibonacciNumbers = numbers
    .Select(number => Fibonacci(number))
    .ToArray();

watch.Stop();
WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed milliseconds");

Write("Results:");
foreach (int number in fibonacciNumbers)
{
    Write($" {number}");
}

/// <summary>
/// Performs the Fibonacci sequence
/// </summary>
static int Fibonacci(int term) =>
    term switch
    {
        1 => 0,
        2 => 1,
        _ => Fibonacci(term - 1) + Fibonacci(term - 2)
    };


