using System;
using System.Collections.Generic;
using System.Text;

namespace HybridTrackerApp.Models
{
    public class AttendanceRecord
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        // Office or Remote
        public string Location { get; set; } = string.Empty;

    }
}
