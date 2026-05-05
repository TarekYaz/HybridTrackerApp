
using SQLite;

namespace HybridTrackerApp.Models
{
    public class AttendanceRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

    }
}
