using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace FoodFit.ViewModels
{
    public partial class SleepTrackViewModel : ObservableObject
    {
        [ObservableProperty]
        private SleepLog selectedSleepHistory;
    }
}