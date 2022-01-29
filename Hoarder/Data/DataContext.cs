using Hoarder.Models;
using Microsoft.EntityFrameworkCore;

namespace Hoarder.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }

    public DbSet<TitleModel> Titles { get; set; }
}