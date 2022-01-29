using System.Linq;
using Hoarder.Attributes;
using Hoarder.Data;
using Hoarder.Models;
using Hoarder.Services;

namespace Hoarder.Hoarders;

[HoarderTask]
public class HackerNewsHoarder
{
    private readonly string url = "https://news.ycombinator.com/";

    public HackerNewsHoarder(IServiceScopeFactory serviceScopeFactory)
    {
        using (var scope = serviceScopeFactory.CreateScope())
        {
            var htmlService = scope.ServiceProvider.GetService<HtmlService>();
            var doc = htmlService.Fetch(url);
            var nodes = doc.DocumentNode.SelectNodes("//tr[@class='athing']").ToList();

            var titles = new List<string>();
            foreach (var node in nodes)
            {
                var res = node.Descendants()
                    .Where(n => n.GetClasses().Contains("titlelink"))
                    .Select(t => t.InnerText);

                titles.AddRange(res);
            }

            var context = scope.ServiceProvider.GetService<DataContext>();
            var titlesInDb = context.Titles.ToList();
            var newTitles = titles.Select(t => new TitleModel(t))
                    .Where(t => !titlesInDb.Contains(t))
                    .ToList();
            context.Titles.AddRange(newTitles);
            context.SaveChanges();
        }
    }
}