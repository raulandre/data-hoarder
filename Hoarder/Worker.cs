using System.Linq;
using System.Reflection;
using Hoarder.Attributes;
using Hoarder.Configuration;
using Hoarder.Data;
using Hoarder.Services;

namespace Hoarder;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly Interval _interval;
    private readonly IServiceScopeFactory _serviceScopeFactory;


    public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _interval = GetInterval();
        _serviceScopeFactory = serviceScopeFactory;
    }

    private Interval GetInterval()
    {
        var interval = new Interval();
        _configuration.GetSection("Interval").Bind(interval);
        return interval;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var hoarderTasks = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsDefined(typeof(HoarderTask)));

        while (!stoppingToken.IsCancellationRequested)
        {
            foreach(var hoarderTask in hoarderTasks)
            {
                _logger.LogInformation($"Running {hoarderTask.Name}");
                Activator.CreateInstance(hoarderTask, args: _serviceScopeFactory);
            }
            
            await Task.Delay((int)_interval.AsTimeSpan().TotalMilliseconds);
        }
    }
}