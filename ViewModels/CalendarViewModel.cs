using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HybridTrackerApp.Models;
using HybridTrackerApp.Services;
using Plugin.Maui.Calendar.Models;

namespace HybridTrackerApp.ViewModels
{
    public partial class CalendarViewModel : ObservableObject
    {
        private readonly DatabaseService _db;

        [ObservableProperty]
        public static string _name = "In Office";
        
        [ObservableProperty]
        public static string _description = "You're checked in on this day!";
       
        [ObservableProperty]
        private EventCollection _events;

        
        private readonly EventModel inOfficeEvent = new() { Name = _name, Description = _description };


        public CalendarViewModel(DatabaseService db)
        {
            _events = new EventCollection();
            _db = db;
        }

        [RelayCommand]
        public async Task DayTapped(DateTime date)
        {
            // Handle day tapped event here
            if (_events.TryGetValue(date, out var eventsForDay))
            {
                bool confirm = await Shell.Current.DisplayAlertAsync("Remove Check-In", "Do you want to remove your check-in for this day?", "Yes", "No");
                if (confirm)
                {
                    _events.Remove(date);
                    await _db.DeleteAttendanceAsync(date);

                }
            } 
            else
            {
                _events.Add(date, new List<EventModel> { inOfficeEvent });
                AttendanceRecord record = new AttendanceRecord
                {
                    Date = date
                };
                await _db.SaveAttendanceAsync(record);
            }
        }


    }
}
