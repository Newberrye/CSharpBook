using System.Diagnostics; //Stopwatch

using static System.Console;
using static System.Diagnostics.Process; // GetCurrentProcess()

namespace Packt.Shared;

public static class Recorder
{
    private static Stopwatch timer = new();

    // Holds the bytes before application starts running
    private static long bytesPhysicalBefore = 0;
    private static long bytesVirtualBefore = 0;

    /// <summary>
    /// Garbage collects before hand. Sets the bytes before application truly begins.
    /// </summary>
    public static void Start()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // store current physical and virtual memory use
        bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
        bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
        timer.Restart();
    }


    /// <summary>
    /// Stops the application, records all used memory, and displays test results to console.
    /// </summary>
    public static void Stop()
    {
        timer.Stop();
        long bytesPhysicalAfter = GetCurrentProcess().WorkingSet64;
        
        long bytesVirtualAfter = GetCurrentProcess().VirtualMemorySize64;

        WriteLine($"{bytesPhysicalAfter - bytesPhysicalBefore:N0} physical bytes were used.");

        WriteLine($"{bytesVirtualAfter - bytesVirtualBefore:N0} virtual bytes were used.");

        WriteLine($"{timer.Elapsed} time span ellapsed");

        WriteLine($"{timer.ElapsedMilliseconds:N0} total milliseconds ellapsed.");
    }
}