using Variables;

object height = 1.88; // storing double in an object
object name = "Amir"; // storing a string in an object
Console.WriteLine($"{name} is {height} metres tall.");

// int length1 = name.Length; // gives compile error!
int length2 = ((string)name).Length; // tell compiler it is a string
Console.WriteLine($"{name} has {length2} characters.");

// storing a string in a dynamic object
// string has Length property
dynamic something = "Ahmed";

// int does not have a Length property
// something = 12;

// an array of any type has a Length property
something = new[] { 3, 5, 7 };

// this compiles but throws an exception at run-time
    // if variable does not have Length property
Console.WriteLine($"Length is {something.Length}");