using FoodFit.Models; 
namespace FoodFit.Views;

public partial class NutritionFactsPage : ContentPage
{
    public NutritionFactsPage(FoodItem selectedFood)
    {
        InitializeComponent();
        BindingContext = selectedFood;
    }
}
