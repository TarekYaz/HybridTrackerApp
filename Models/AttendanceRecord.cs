using SQLite;

namespace HybridTrackerApp.Models
{
    public class AttendanceRecord
    {
        [PrimaryKey]
        public DateTime Date { get; set; }

        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
    }
}
