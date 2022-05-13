// #error version
// Console.WriteLine("Hello, World!");

using System.Reflection;

// Random unused variable for additional assemblies
System.Data.DataSet ds;
HttpClient client;

Assembly? assembly = Assembly.GetEntryAssembly();
    if (assembly == null) return;

// Loop through the asemblies that this app references
foreach (AssemblyName name in assembly.GetReferencedAssemblies())
{
    // Load the assembly so we can read it.
    Assembly a = Assembly.Load(name);

    // Declare variable to count number of methods
    int methodCount = 0;

    // Loop through all the types in the assembly
    foreach (TypeInfo t in a.DefinedTypes)
    {
        // add up counts of methods
        methodCount += t.GetMethods().Count();
    }

    // Output count types and their methods
    Console.WriteLine(
        "{0:N0} types with {1:N0} methods in {2} assembly.",
        arg0: a.DefinedTypes.Count(),
        arg1: methodCount, arg2: name.Name);
}
