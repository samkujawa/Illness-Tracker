using SQLite;
using Microsoft.Maui.Graphics;

namespace SymptomTracker;

public class Symptom
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public int Intensity { get; set; }
    public string Notes { get; set; }

    // To handle color-coding based on intensity
    [Ignore]
    public Color DisplayColor
    {
        get
        {
            if (Intensity >= 1 && Intensity <= 3) return Colors.Green;
            if (Intensity >= 4 && Intensity <= 7) return Colors.Orange;
            return Colors.Red; // for 8, 9, 10
        }
    }

    [Ignore]
    public string DisplayTime
    {
        get
        {
            DateTime baseDate = DateTime.Today; // Start at today's date.
            return baseDate.Add(Time).ToString("h:mm tt");
        }
    }

}
