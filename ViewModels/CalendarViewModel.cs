using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Calendar.Models;
using HybridTrackerApp.Models;
using CommunityToolkit.Mvvm.Input;

namespace HybridTrackerApp.ViewModels
{
    public partial class CalendarViewModel : ObservableObject
    {
        
        [ObservableProperty]
        public static string _name = "In Office";
        
        [ObservableProperty]
        public static string _description = "You're checked in on this day!";
       
        [ObservableProperty]
        private EventCollection _events;

        
        private readonly EventModel inOfficeEvent = new() { Name = _name, Description = _description };


        public CalendarViewModel()
        {
            _events = new EventCollection();
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
                }
            } 
            else
            {
                _events.Add(date, new List<EventModel> { inOfficeEvent });
            }
        }


    }
}
