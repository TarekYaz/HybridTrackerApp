using CommunityToolkit.Mvvm.ComponentModel;
using HybridTrackerApp.Models;

namespace HybridTrackerApp.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _userName = "Alex Doe";

        [ObservableProperty]
        private string _todayDate = DateTime.Now.ToString("ddd dd MMM");

        [ObservableProperty]
        private bool _isCheckedIn = true;

        public DashboardViewModel()
        {
            
        }

    }
}
