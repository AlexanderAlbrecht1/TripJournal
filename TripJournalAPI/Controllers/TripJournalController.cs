using Microsoft.AspNetCore.Mvc;
using TripJournalAPI.Models;
using TripJournalAPI.Data;

namespace TripJournalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripJournalController : ControllerBase

    
{
    private readonly JournalContext _context;

    public TripJournalController(JournalContext context)
    {
        _context = context;
    }

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
        return Ok(_context.TripDays.ToList() );
    }

    [HttpPost]
    public ActionResult<TripDay> AddEntry([FromBody] TripDay entry)
    {
        _context.TripDays.Add(entry);
        _context.SaveChanges();
        return Ok(entry);
    }

    [HttpPut("{id}")]
    public ActionResult<TripDay> UpdateEntry([FromBody] TripDay entry, int id)
    {
        if (entry.Id == id)
        {
            var existingEntry = _context.TripDays.Find(id);
            if (existingEntry != null)
            {
                existingEntry.Date = entry.Date;
                existingEntry.Weather = entry.Weather;
                existingEntry.Temperature = entry.Temperature;
                existingEntry.Activities = entry.Activities;
                existingEntry.Notes = entry.Notes;
                existingEntry.Mood = entry.Mood;

                _context.SaveChanges();
                return Ok(existingEntry);
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return BadRequest("Die ID in der URL stimmt nicht mit der ID im Objekt überein.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEntry(int id)
    {
        var entry = _context.TripDays.Find(id);
        if (entry != null)
        {
            _context.TripDays.Remove(entry);
            _context.SaveChanges();
            return NoContent();
        }
        else
        {
            return NotFound("Der Eintrag wurde nicht gefunden.");
        }
    }
}

