using FoodFit.Models;
using FoodFit.ViewModels;
using System.Collections.ObjectModel;

namespace FoodFit.Views
{
    public partial class SearchPage : ContentPage
    {
        private readonly LocalDBService _localDbService;
        private readonly userViewModel _userViewModel;

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

        public SearchPage(LocalDBService dbService, userViewModel userViewModel)
        {
            InitializeComponent();
            _localDbService = dbService;
            _userViewModel = userViewModel;
            BindingContext = this;

        }

        private async void FilterFoods()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                // If the search text is empty, clear the filtered list
                FilteredFoods.Clear();
                return;
            }
            var filtered = await _localDbService.GetFoodsByName(SearchText.Trim());
            FilteredFoods.Clear();
            foreach (var food in filtered)
            {
                FilteredFoods.Add(food);
            }
        }


        private async Task FilterFoodsAsync()
        {
            var filtered = await _localDbService.GetFoodsByName(SearchText);
            FilteredFoods.Clear();
            foreach (var food in filtered)
            {
                FilteredFoods.Add(food);
            }
        }
        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is FoodFit.Models.Foods selectedFood)
            {
                await Navigation.PushAsync(new FoodDetailPage(selectedFood, _localDbService, _userViewModel));
            }
        }
    }
}
