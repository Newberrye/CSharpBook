using System.Diagnostics;
using Microsoft.Extensions.Configuration;

// static double Add(double a, double b)
// {
//    return a + b;
// }

// double a = 4.5;
// double b = 2.5;
// double answer = Add(a, b);
// WriteLine($"{a} + {b} = {answer}");
// WriteLine("Press ENTER to end the app.");
// ReadLine();

ConfigurationBuilder builder = new();

builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfiguration configuration = builder.Build();

TraceSwitch ts = new(
    displayName: "PacktSwitch",
    description: "This switch is aset via a JSON config.");

configuration.GetSection("PacktSwitch").Bind(ts);

Trace.WriteLineIf(ts.TraceError, "Trace Error");
Trace.WriteLineIf(ts.TraceWarning, "Trace Warning");
Trace.WriteLineIf(ts.TraceInfo, "Trace information");
Trace.WriteLineIf(ts.TraceVerbose, "Trace verbose");