using static System.Console;

Task outerTask = Task.Factory.StartNew(OuterMethod);
outerTask.Wait();
WriteLine("Console app is stopping.");

/// <summary>
/// Parent function that requires child task to complete first
/// </summary>
static void OuterMethod()
{
    WriteLine("Outer method starting...");
    Task innerTask = Task.Factory.StartNew(InnerMethod, TaskCreationOptions.AttachedToParent);
    WriteLine("Outer method finished.");
}

/// <summary>
/// Child function
/// </summary>
static void InnerMethod()
{
    WriteLine("Inner method starting...");
    Thread.Sleep(2000);
    WriteLine("Inner method finished.");
}