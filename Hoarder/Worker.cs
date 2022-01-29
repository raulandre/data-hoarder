using System.Linq;
using System.Reflection;
using Hoarder.Attributes;
using Hoarder.Configuration;
using Hoarder.Services;

namespace Hoarder;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly Interval _interval;

    private List<Task> _tasks = new();

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _interval = GetInterval();
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
                Activator.CreateInstance(hoarderTask);
            }
            
            await Task.Delay((int)_interval.AsTimeSpan().TotalMilliseconds);
        }
    }
}