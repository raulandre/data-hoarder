using Hoarder;
using Hoarder.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<HtmlService>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
