
using SQLite;

namespace HybridTrackerApp.Models
{
    public class AttendanceRecord
    {
        //public int Id { get; set; }

        [PrimaryKey]
        public DateTime Date { get; set; }

    }
}
