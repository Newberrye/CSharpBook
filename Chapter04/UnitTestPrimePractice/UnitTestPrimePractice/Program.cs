using UnitTestPrimePractice;

Primefactors primeObject = new Primefactors();
int prime = 2_000;
string primes = primeObject.PrimeFactors(prime);
string message = $"Primes of {prime} are: {primes}";
Console.WriteLine(message);
