using System.Reflection; // Assembly
using static System.Console;

WriteLine("Assembly metadata:");
Assembly? assembly = Assembly.GetEntryAssembly();
if (assembly is null)
{
    WriteLine("Failed to get entry assembly.");
    return;
}
WriteLine($" Full name: {assembly.FullName}");
WriteLine($" Location: {assembly.Location}");
IEnumerable<Attribute> attributes = assembly.GetCustomAttributes();
WriteLine($" Assembly-level attributes:");
foreach (Attribute a in attributes)
{
    WriteLine($" {a.GetType()}");
}

AssemblyInformationalVersionAttribute? version = assembly
 .GetCustomAttribute<AssemblyInformationalVersionAttribute>();
WriteLine($" Version: {version?.InformationalVersion}");
AssemblyCompanyAttribute? company = assembly
 .GetCustomAttribute<AssemblyCompanyAttribute>();
WriteLine($" Company: {company?.Company}");
