using System;
using System.Collections.Generic;
using System.Text;

namespace HybridTrackerApp.Models
{
    public class UserProfile
    {
        public string UserId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;

        // Target pertcentage for attendance in the office.
        public double TargetPercentage { get; set; } = 40.0;

        public bool AutoCheckInEnabled { get; set; } = true;


    }
}
