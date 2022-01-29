using Hoarder.Attributes;

namespace Hoarder.Hoarders;

[HoarderTask]
public class HackerNewsHoarder
{
    public HackerNewsHoarder()
    {
        Console.WriteLine(nameof(HackerNewsHoarder));
    }
}