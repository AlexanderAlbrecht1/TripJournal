using Microsoft.EntityFrameworkCore;
using TripJournalAPI.Models;

namespace TripJournalAPI.Data;

public class JournalContext : DbContext
{
    public JournalContext(DbContextOptions<JournalContext> options) : base(options)
    {
    }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<TripDay> TripDays { get; set; }
}