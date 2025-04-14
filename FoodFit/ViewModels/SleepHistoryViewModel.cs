
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FoodFit.ViewModels
{
    partial class SleepHistoryViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<SleepLog> sleepHistories = new();
        [ObservableProperty]
        private SleepLog selectedSleepHistory = new();

        [RelayCommand]
        private void Add()
        {
            SleepHistories.Add(SelectedSleepHistory); 
            SelectedSleepHistory = new SleepLog();    
        }
    }
}
