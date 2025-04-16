using FoodFit.Models;

namespace FoodFit.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent(); 
		BindingContext = new SearchViewModel();
    }
    private async void OnFoodSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FoodItem selectedFood)
        {
            ((CollectionView)sender).SelectedItem = null;
            await Navigation.PushAsync(new NutritionFactsPage(selectedFood));
        }
    }

}