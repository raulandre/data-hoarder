using Hoarder;
using Hoarder.Data;
using Hoarder.Services;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<HtmlService>();
        services.AddDbContext<DataContext>(opt => opt.UseSqlite("Data Source = data.db"));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
