using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Calendar.Models;
using HybridTrackerApp.Models;
using CommunityToolkit.Mvvm.Input;

namespace HybridTrackerApp.ViewModels
{
    public partial class CalendarViewModel : ObservableObject
    {
        [ObservableProperty]
        private EventCollection _events;
        //[ObservableProperty]
        //public EventCollection _events = new()
        //{
        //    [DateTime.Now] = new List<EventModel>
        //    {
        //        new() { Name = "Cool event1", Description = "This is Cool event1's description!" },
        //        new() { Name = "Cool event2", Description = "This is Cool event2's description!" }
        //    },
        //    // 5 days from today
        //    [DateTime.Now.AddDays(5)] = new List<EventModel>
        //    {
        //        new() { Name = "Cool event3", Description = "This is Cool event3's description!" },
        //        new() { Name = "Cool event4", Description = "This is Cool event4's description!" }
        //    },
        //    // 3 days ago
        //    [DateTime.Now.AddDays(-3)] = new List<EventModel>
        //    {
        //        new() { Name = "Cool event5", Description = "This is Cool event5's description!" }
        //    },
        //    // custom date
        //    [new DateTime(2024, 3, 16)] = new List<EventModel>
        //    {
        //        new() { Name = "Cool event6", Description = "This is Cool event6's description!" }
        //    }
        //};

        public CalendarViewModel()
        {
            _events = new EventCollection();
        }

        [RelayCommand]
        public void DayTapped(DateTime date)
        {
            // Handle day tapped event here
            if (_events.TryGetValue(date, out var eventsForDay))
            {
                // Do something with the events for the tapped day
                // For example, navigate to a details page or show a popup
                _events.Remove(date);
            } else
            {
                _events.Add(date, new List<EventModel>
                {
                    new() { Name = "In Office", Description = string.Format("You've checked in on: {0}", date) }
                });
            }
        }


    }
}
