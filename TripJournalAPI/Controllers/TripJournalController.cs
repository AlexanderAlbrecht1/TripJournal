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

    [HttpPost]
    public ActionResult<IEnumerable<TripDay>> AddEntry([FromBody] TripDay entry)
    {
        entry.Id = _entries.Any() ? _entries.Max(e => e.Id) + 1 : 1;
        _entries.Add(entry);
        return Ok(entry);
    }

    [HttpPut("{id}")]
    public ActionResult<TripDay> UpdateEntry([FromBody] TripDay entry, int id)
    {
        if (entry.Id == id)
        {
            var index = _entries.FindIndex(e => e.Id == id);
            if (index >= 0)
            {
                _entries[index] = entry;
                return Ok(entry);
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
        var index = _entries.FindIndex(e => e.Id == id);
        if (index >= 0)
        {
            _entries.RemoveAt(index);
            return NoContent();
        }
        else
        {
            return NotFound("Der Eintrag wurde nicht gefunden.");
        }
    }
}

