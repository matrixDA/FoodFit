using System.Diagnostics;
using FoodFit.Models;
using FoodFit.ViewModels;

namespace FoodFit.Views;

public partial class FoodDetailPage : ContentPage
{
    public Foods SelectedFood { get; set; }
    private readonly FoodDetailViewModel _viewModel;
    private readonly LocalDBService _localDBService;
    private readonly userViewModel _userViewModel;
    public FoodDetailPage(Foods food, LocalDBService localDBService, userViewModel userViewModel)
    {
        InitializeComponent();
        _viewModel = new FoodDetailViewModel(food);
        BindingContext = _viewModel;
        _localDBService = localDBService;
        _userViewModel = userViewModel;
    }

    private async void AddToLogButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.Servings <= 0)
        {
            await DisplayAlert("Error", "Please enter a valid number of servings.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(_viewModel.SelectedMealType))
        {
            await DisplayAlert("Error", "Please select a meal type.", "OK");
            return;
        }

        var mealType = mealTypePicker.SelectedItem.ToString();

        var foodLog = new FoodLog
        {
            UserId = _userViewModel.UserId,
            FoodName = _viewModel.SelectedFood.FoodName,
            Quantity = _viewModel.Servings,
            Unit = _viewModel.SelectedFood.Unit,
            Caloroies = _viewModel.SelectedFood.Calories * _viewModel.Servings,
            Protein = _viewModel.SelectedFood.Protein * _viewModel.Servings,
            Carbs = _viewModel.SelectedFood.Carbs * _viewModel.Servings,
            Fat = _viewModel.SelectedFood.Fat * _viewModel.Servings,
            MealType = Enum.Parse<MealType>(_viewModel.SelectedMealType),
            EntryDate = DateTime.Now.Date
        };

        await _localDBService.CreateFoodLogEntry(foodLog);
        await _localDBService.UpdateDailyNutrition(_userViewModel.UserId, DateTime.Now.Date, _viewModel.SelectedFood.Calories * _viewModel.Servings,
            _viewModel.SelectedFood.Protein * _viewModel.Servings, _viewModel.SelectedFood.Carbs * _viewModel.Servings,
            _viewModel.SelectedFood.Fat * _viewModel.Servings);

        await DisplayAlert("Success", $"{_viewModel.SelectedFood.FoodName} added to your journal.", "OK");

         await Navigation.PopAsync();

    }
}
