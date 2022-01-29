using System.ComponentModel.DataAnnotations;

namespace Hoarder.Models;

public class TitleModel : IEquatable<TitleModel>
{
    [Key]
    public long Id { get; set; }
    public string Title { get; set; }
    public DateTime Timestamp { get; set; }

    public TitleModel(string title)
    {
        Id = 0;
        Title = title;
        Timestamp = DateTime.UtcNow; 
    }

    public static implicit operator TitleModel(string title)
        => new TitleModel(title);

    public bool Equals(TitleModel other)
    {
        return Title == other.Title;
    }
}