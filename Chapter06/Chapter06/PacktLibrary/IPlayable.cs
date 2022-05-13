// C#8 default implementations with interfaces
using static System.Console;

namespace Packt.Shared;

public interface IPlayable
{
    void Play();
    void Pause();

    void Stop()
    {
        WriteLine("Default implementation of Stop.");
    }
}
