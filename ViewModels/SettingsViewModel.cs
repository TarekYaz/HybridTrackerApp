using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HybridTrackerApp.Services;

namespace HybridTrackerApp.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly DatabaseService _db;

        public SettingsViewModel(DatabaseService db)
        {
            _db = db;
        }

        [RelayCommand]
        public async Task DeleteAllDataAsync()
        {
            bool confirm = await Shell.Current.DisplayAlertAsync("Delete All Data", "Are you sure you want to delete all your check-in data? This action cannot be undone.", "Yes", "No");
            if (confirm)
            {
                await _db.DeleteAllAttendanceAsync();
                await Shell.Current.DisplayAlertAsync("Data Deleted", "All your check-in data has been deleted.", "OK");
            }
        }
    }
}
