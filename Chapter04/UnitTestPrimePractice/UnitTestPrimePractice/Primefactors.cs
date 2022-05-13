
namespace UnitTestPrimePractice;

internal class Primefactors
{
    public string PrimeFactors(int number)
    {
        string primeList = "";

        int div = 2;
        while (number > 1)
        {
            if (number % div == 0)
            {
                primeList += $"{div} ";
                number /= div;
            }
            else
                div++;
        }

        return primeList;
    }
}
