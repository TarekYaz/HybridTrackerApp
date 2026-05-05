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
            _db = db;
            _events = new EventCollection();

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Events = await _db.LoadEventCollectionAsync();
        }

        [RelayCommand]
        public async Task DayTapped(DateTime date)
        {
            if (Events.TryGetValue(date, out var eventsForDay))
            {
                bool confirm = await Shell.Current.DisplayAlertAsync("Remove Check-In", "Do you want to remove your check-in for this day?", "Yes", "No");
                if (confirm)
                {
                    Events.Remove(date);
                    await _db.DeleteAttendanceAsync(date);
                }
            } 
            else
            {
                Events.Add(date, new List<EventModel> { inOfficeEvent });

                // Persist only the first event
                await _db.SaveEventsAsync(date, new List<EventModel> { inOfficeEvent });
            }
        }
    }
}
