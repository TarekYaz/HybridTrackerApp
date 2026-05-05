using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HybridTrackerApp.Models;
using HybridTrackerApp.Services;

namespace HybridTrackerApp.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly DatabaseService _db;

        [ObservableProperty]
        private string _userName = "Alex Doe";

        [ObservableProperty]
        private string _todayDate = DateTime.Now.ToString("ddd dd MMM");

        [ObservableProperty]
        private bool _isCheckedIn = false;

        [ObservableProperty]
        private double _attendancePercentage = 0.0;

        [ObservableProperty]
        private int _officeDays = 0;

        public DashboardViewModel(DatabaseService db)
        {
            _db = db;
        }

        [RelayCommand]
        public async Task LoadDataAsync()
        {
            var todayRecord = await _db.GetTodayAttendanceAsync();
            IsCheckedIn = todayRecord != null;
            await CalculateAttendanceAsync();
        }

        [RelayCommand]
        public async Task CheckInAsync()
        {
            if (!_isCheckedIn)
            {
                var record = new AttendanceRecord { Date = DateTime.Today };
                await _db.SaveAttendanceAsync(record);
                IsCheckedIn = true;

                await CalculateAttendanceAsync();
            }
        }

        [RelayCommand]
        public async Task CheckOutAsync()
        {
            if (IsCheckedIn)
            {
                DateTime Date = DateTime.Today;
                await _db.DeleteAttendanceAsync(Date);
                IsCheckedIn = false;
                await CalculateAttendanceAsync();
            }
        }

        private async Task CalculateAttendanceAsync()
        {
            var CurrentMonthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var CurrentMonthEnd = CurrentMonthStart.AddMonths(1).AddDays(-1);

            List<AttendanceRecord> attendanceRecords = await _db.GetAttendanceAsync(CurrentMonthStart, CurrentMonthEnd);

            OfficeDays = attendanceRecords.Count();

            // Count working days in the current month
            int workingDays = Enumerable.Range(0, (CurrentMonthEnd - CurrentMonthStart).Days + 1)
                .Select(i => CurrentMonthStart.AddDays(i))
                .Count(d => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday);

            // Work out percentage if not 0
            AttendancePercentage = workingDays > 0
                ? Math.Round((double)OfficeDays / workingDays * 100, 1)
                : 0;

        }
    }
}
