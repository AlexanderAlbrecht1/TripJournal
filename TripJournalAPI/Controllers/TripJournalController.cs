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

    [HttpGet]
    public ActionResult<IEnumerable<TripDay>> GetAllEntries()
    {
        // Ok() sendet HTTP Status 200 zurück mitsamt unserer Liste
        return Ok(_context.TripDays.ToList());
    }

    [HttpPost]
    public ActionResult<TripDay> AddEntry([FromBody] TripDay entry)
    {
        var tripExists = _context.Trips.Any(t => t.Id == entry.TripId);
        if (!tripExists)
        {
            return BadRequest($"Fehler: Ein Trip mit der ID {entry.TripId} existiert nicht.");
        }

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

