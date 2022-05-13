using static System.Console;

HttpClient client = new();

HttpResponseMessage response =
    await client.GetAsync("http://www.apple.com");

WriteLine($"Apple's home page has {response.Content.Headers.ContentLength:N0} bytes.");
