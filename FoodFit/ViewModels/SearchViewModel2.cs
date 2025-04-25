using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FoodFit.Models;

namespace FoodFit.ViewModels
{
    public partial class SearchViewModel2 : ObservableObject
    {
        private readonly LocalDBService _dbService;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private ObservableCollection<Foods> filteredFoods;

        public SearchViewModel2(LocalDBService dbService)
        {
            _dbService = dbService;
            FilteredFoods = new ObservableCollection<Foods>();
           // LoadFoodsCommand = new AsyncRelayCommand(LoadFoodsAsync);
            SearchCommand = new AsyncRelayCommand(FilterFoodsAsync);
        }

        public IAsyncRelayCommand LoadFoodsCommand { get; }
        public IAsyncRelayCommand SearchCommand { get; }

        private async Task LoadFoodsAsync()
        {
            var allFoods = await _dbService.GetAllFoods();
            FilteredFoods.Clear();
            foreach (var food in allFoods)
            {
                FilteredFoods.Add(food);
            }
        }

        private async Task FilterFoodsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadFoodsAsync();
                return;
            }

            var filtered = await _dbService.GetFoodsByName(SearchText);
            FilteredFoods.Clear();
            foreach (var food in filtered)
            {
                FilteredFoods.Add(food);
            }
        }
    }
}

