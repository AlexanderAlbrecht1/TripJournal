using Microsoft.EntityFrameworkCore;
using TripJournalAPI.Models;

namespace TripJournalAPI.Data;

public class JournalContext : DbContext
{
    public JournalContext(DbContextOptions<JournalContext> options) : base(options)
    {
    }

    public DbSet<TripDay> TripDays { get; set; }
}