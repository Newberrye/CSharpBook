using static System.Console;

int numberOfApples = 12;
decimal pricePetApple = 0.35M;

WriteLine(
    format: "{0} apples costs {1:C}",
    arg0: numberOfApples,
    arg1: pricePetApple * numberOfApples);

string formatted = string.Format(
    format: "{0} apples costs {1:C}",
    arg0: numberOfApples,
    arg1: pricePetApple * numberOfApples);

// WriteToeFile(formatted); // writes the string into a file

string applesText = "Apples";
var applesCount = 1234;

string bananasText = "Bananas";
var bananasCount = 56789;

WriteLine($"{"Name",-10} {"Count",6:N0}");
WriteLine($"{applesText,-10} {applesCount,6:N0}");
WriteLine($"{bananasText,-10} {bananasCount,6:N0}");

//Write("Type your first name and press ENTER: ");
//string? firstName = ReadLine();
//Write("Type your age and press ENTER: ");
//string? age = ReadLine();
//WriteLine(
// $"Hello {firstName}, you look good for {age}.");

