using System.Diagnostics; //Stopwatch

using static System.Console;

/// <summary>
/// This program shows how using a 'conch' object can act as a soft lock so only one task may access/modify an object at a time.
/// </summary>

WriteLine("Please wait for the tasks to complete.");
Stopwatch watch = Stopwatch.StartNew();

Task a = Task.Factory.StartNew(MethodA);
Task b = Task.Factory.StartNew(MethodB);

Task.WaitAll(new Task[] { a, b });

WriteLine();
WriteLine($"Results: {SharedObjects.Message}.");
WriteLine($"{watch.ElapsedMilliseconds:N0} elapsed milliseconds.");
WriteLine($"{SharedObjects.Counter} string modifications.");



/// <summary>
/// Random wait time A
/// </summary>
static void MethodA()
{
    try
    {
        if (Monitor.TryEnter(SharedObjects.Conch, TimeSpan.FromSeconds(15)))
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(SharedObjects.Random.Next(2000));
                SharedObjects.Message += "A";
                Write(".");
                Interlocked.Increment(ref SharedObjects.Counter);
            }
        }
        else
        {
            WriteLine("Method A timed out when entering a monitor on conch.");
        }
    }
    finally
    {
        Monitor.Exit(SharedObjects.Conch);
    }
}

/// <summary>
/// Random wait time B
/// </summary>
static void MethodB()
{
    try
    {
        if (Monitor.TryEnter(SharedObjects.Conch, TimeSpan.FromSeconds(15)))
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(SharedObjects.Random.Next(2000));
                SharedObjects.Message += "B";
                Write(".");
                Interlocked.Increment(ref SharedObjects.Counter);
            }
        }
        else
        {
            WriteLine("Method A timed out when entering a monitor on conch.");
        }
    }
    finally
    {
        Monitor.Exit(SharedObjects.Conch);
    }
}

/// <summary>
/// Object with a shared resource the above methods use
/// </summary>
static class SharedObjects
{
    public static Random Random = new();
    public static string? Message; // a shared resource
    public static int Counter; // another shared resource

    public static object Conch = new();

}