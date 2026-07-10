using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripJournalAPI.Data;
using TripJournalAPI.Models;

namespace TripJournalAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly JournalContext _context;

    public TripsController(JournalContext context)
    {
        _context = context;
    }

    [HttpPost]
    public ActionResult<Trip> CreateTrip([FromBody] Trip trip)
    {
        _context.Trips.Add(trip);
        _context.SaveChanges();
        return Ok(trip);
    }
    [HttpGet]
    public ActionResult<IEnumerable<Trip>> GetAllTrips()
    {
        var trips = _context.Trips.Include(t => t.Days).ToList();
        return Ok(trips);
    }
}