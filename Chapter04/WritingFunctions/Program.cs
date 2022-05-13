using static System.Console;

static void TimesTabele(byte number)
{
    WriteLine($"This is the {number} times tables:");

    for (int row = 1; row <= 12; row++)
    {
        WriteLine($"{row} x {number} = {row * number}");
    }
    WriteLine();
}

// TimesTabele(255);

static int FibImperative(int term)
{
    if (term == 1)
    {
        return 0;
    }
    else if (term == 2) 
    {
        return 1;
    }
    else
    {
        return FibImperative(term - 1) + FibImperative(term - 2);
    }
}

static void RunFibImperative()
{
    for (int i = 1; i <= 30; i++)
    {
        WriteLine($"The {i} of Fibonacci sequence is {FibImperative(term: i):N0}.");
    }
}

// RunFibImperative();

static int FibFunctional(int term) =>
 term switch
 {
     1 => 0,
     2 => 1,
     _ => FibFunctional(term - 1) + FibFunctional(term - 2)
 };

static void RunFibFunctional()
{
    for (int i = 1; i <= 30; i++)
    {
        WriteLine("The {0} term of the Fibonacci sequence is {1:N0}.",
        arg0: i,
        arg1: FibFunctional(term: i));
    }
}

RunFibFunctional();
