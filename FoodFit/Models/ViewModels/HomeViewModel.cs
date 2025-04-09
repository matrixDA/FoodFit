using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FoodFit.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        public ObservableCollection<string> FoodCategories { get; } = new()
        {
            "Fruits", "Vegetables", "Dairy", "Meat", "Grains", "Seafood", "Beverages", "Snacks"
        };

        [ObservableProperty]
        private ObservableCollection<string> filteredCategories;

        [ObservableProperty]
        private string searchText;

        public HomeViewModel()
        {
            FilteredCategories = new ObservableCollection<string>(FoodCategories);
        }

        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                FilteredCategories = new ObservableCollection<string>(FoodCategories);
            }
            else
            {
                var filtered = FoodCategories
                    .Where(c => c.ToLower().Contains(value.ToLower()))
                    .ToList();
                FilteredCategories = new ObservableCollection<string>(filtered);
            }
        }
    }
}
