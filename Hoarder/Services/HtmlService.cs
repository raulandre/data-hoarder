using HtmlAgilityPack;

namespace Hoarder.Services;

public class HtmlService
{
    public HtmlDocument Fetch(string url)
    {
        return new HtmlWeb().Load(url);
    }
}