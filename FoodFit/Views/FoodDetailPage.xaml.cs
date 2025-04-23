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
    
}
