using System.ComponentModel.DataAnnotations;

namespace TripJournalAPI.Models;

public class Trip
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public List<TripDay> Days { get; set; } = new List<TripDay>();
}