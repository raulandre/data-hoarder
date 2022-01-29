using System.Linq;
using Hoarder.Attributes;
using Hoarder.Services;

namespace Hoarder.Hoarders;

[HoarderTask]
public class HackerNewsHoarder
{
    private readonly string url = "https://news.ycombinator.com/";

    public HackerNewsHoarder()
    {
        var htmlService = new HtmlService();
        var doc = htmlService.Fetch(url);
        var nodes = doc.DocumentNode.SelectNodes("//tr[@class='athing']").ToList();

        var titles = new List<string>();
        foreach(var node in nodes)
        {
            var res = node.Descendants()
                .Where(n => n.GetClasses().Contains("titlelink"))
                .Select(t => t.InnerText);

            titles.AddRange(res);
        }

        var path = Path.Combine(Directory.GetCurrentDirectory(), "out.txt");
        if(File.Exists(path))
        {
            var lines = File.ReadAllLines(path);
            foreach(var line in lines)
                titles = titles.Where(t => !t.Contains(line)).ToList();
            File.AppendAllLines(path, titles);
        } else File.WriteAllLines(path, titles);
    }
}