using Microsoft.AspNetCore.Mvc;
using TripJournalAPI.Models;

namespace TripJournalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripJournalController : ControllerBase
{
    // Wir erstellen eine feste Liste im Speicher für den ersten Test
    private static readonly List<TripDay> _entries = new List<TripDay>
    {
        new TripDay
        {
            Id = 1,
            Date = DateTime.Now.AddDays(-1),
            Weather = "Sunny",
            Temperature = 25,
            Activities = "Sightseeing in Barcelona",
            Notes = "Found a beautiful small cafe.",
            Mood = "Happy"
        },
        new TripDay
        {
            Id = 2,
            Date = DateTime.Now,
            Weather = "Rainy",
            Temperature = 18,
            Activities = "Visited Picasso Museum",
            Notes = "Queues were long, but worth it.",
            Mood = "Tired but inspired"
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<TripDay>> GetAllEntries()
    {
        // Ok() sendet HTTP Status 200 zurück mitsamt unserer Liste
        return Ok(_entries);
    }
}