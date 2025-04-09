
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FoodFit.ViewModels
{
    public partial class SearchViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> filteredCategories;

        [ObservableProperty]

        private string searchText;

    }
}
