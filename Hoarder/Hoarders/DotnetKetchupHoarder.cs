using Hoarder.Attributes;

namespace Hoarder.Hoarders;

[HoarderTask]
public class DotnetKetchupHoarder
{
    public DotnetKetchupHoarder()
    {
        System.Console.WriteLine(nameof(DotnetKetchupHoarder));
    }
}