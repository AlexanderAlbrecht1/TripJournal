using System.ComponentModel.DataAnnotations;

namespace TripJournalAPI.Models;

public class TripDay
{
   public int Id {get;set; }
   
   [Required(ErrorMessage = "Datum ist erforderlich")]
   public DateTime? Date{get;set;}
   public string Weather {get;set;} = string.Empty;
   public int Temperature {get;set;}
   public string Activities {get; set;} = string.Empty;
   public string Notes {get; set;} = string.Empty;
   public string Mood {get; set;} = string.Empty;   
   public int TripId { get; set; }
   public Trip? Trip { get; set; }
}