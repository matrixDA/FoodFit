using FoodFit.Models; 

namespace FoodFit.Views;

public partial class FoodDetailPage : ContentPage
{
    public Foods SelectedFood { get; set; }

    public FoodDetailPage(Foods food)
    {
        InitializeComponent();
        SelectedFood = food;
        BindingContext = this;
    }

    private async void AddToLogButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Added to Log", $"{SelectedFood.FoodName} has been added to your log.", "OK");
    }
}
