using FoodFit.Models;
using System.Collections.ObjectModel;

namespace FoodFit.Views
{
    public partial class SearchPage : ContentPage
    {
        private readonly LocalDBService _dbService;

        public ObservableCollection<Foods> FilteredFoods { get; set; } = new ObservableCollection<Foods>();
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    FilterFoods();
                }
            }
        }
        private string _searchText;

        public SearchPage(LocalDBService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            BindingContext = this;

        }

        private async void FilterFoods()
        {
            FilteredFoods.Clear();
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? await _dbService.GetAllFoods()
                : await _dbService.GetFoodsByName(SearchText);

            foreach (var food in filtered)
            {
                FilteredFoods.Add(food);
            }
        }


        private async Task FilterFoodsAsync()
        {
            var filtered = await _dbService.GetFoodsByName(SearchText);
            FilteredFoods.Clear();
            foreach (var food in filtered)
            {
                FilteredFoods.Add(food);
            }
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
