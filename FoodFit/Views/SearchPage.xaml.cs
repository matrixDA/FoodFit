using FoodFit.ViewModels;

namespace FoodFit.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent(); 
		BindingContext = new SearchViewModel();
    }
}