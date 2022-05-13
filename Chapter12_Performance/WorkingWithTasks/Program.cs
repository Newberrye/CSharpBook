using System.Diagnostics; //Stopwatch

using static System.Console;


OutputThreadInfo();
Stopwatch timer = Stopwatch.StartNew();

// Running methods on 1 thread
/*
WriteLine("Running methods synchronously on one thread.");
MethodA();
MethodB();
MethodC();
*/

/*
WriteLine("Running methods asynchronously on multiple threads.");
Task taskA = new(MethodA);
taskA.Start();
Task taskB = Task.Factory.StartNew(MethodB);
Task taskC = Task.Run(MethodC);

Task[] tasks = { taskA, taskB, taskC }; // array of all tasks
Task.WaitAll(tasks); // wait for all tasks completed.
*/

WriteLine("Passing the result of one task as an input into another.");

Task<string> taskServiceThenSProc = Task.Factory
    .StartNew(CallWebService) // return Task<decimal>
    .ContinueWith(previousTask => // returns Task<string>
        CallStoredProcedure(previousTask.Result));

WriteLine($"Result: {taskServiceThenSProc.Result}");

WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");

/// <summary>
/// Writes current thread information in Console.
/// </summary>
static void OutputThreadInfo()
{
    Thread t = Thread.CurrentThread;
    WriteLine(
        "Thread Id: {0}, Priority: {1}, Background: {2}, Name: {3}",
        t.ManagedThreadId, t.Priority,
        t.IsBackground, t.Name ?? "null");
}

/// <summary>
/// Simulates work for one thread
/// </summary>
static void MethodA()
{
    WriteLine("Starting Method A...");
    OutputThreadInfo();
    Thread.Sleep(3000); // simulate three seconds of work
    WriteLine("Finished Method A.");
}

/// <summary>
/// Simulates work for second thread.
/// </summary>
static void MethodB()
{
    WriteLine("Starting Method B...");
    OutputThreadInfo();
    Thread.Sleep(2000); // simulate two seconds of work
    WriteLine("Finished Method B.");
}

/// <summary>
/// Simulates work for third thread.
/// </summary>
static void MethodC()
{
    WriteLine("Starting Method C...");
    OutputThreadInfo();
    Thread.Sleep(1000); // simulate one second of work
    WriteLine("Finished Method C.");
}

/// <summary>
/// Simulates data web service call that needs to be done before CallStoredProcedure
/// </summary>
static decimal CallWebService()
{
    WriteLine("Starting call to web service...");
    OutputThreadInfo();
    Thread.Sleep((new Random()).Next(2000, 4000));
    WriteLine("Finished call to web service.");

    return 89.99M;
}

/// <summary>
/// Simulates database querry
/// </summary>
static string CallStoredProcedure(decimal amount)
{
    WriteLine("Starting call to stored procedure...");
    OutputThreadInfo();
    Thread.Sleep((new Random()).Next(2000, 4000));
    WriteLine("Finished call to stored procedure.");

    return $"12 products cost more than {amount:C}.";
}