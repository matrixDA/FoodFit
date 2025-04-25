using System.Diagnostics;
using FoodFit.Models;
using FoodFit.ViewModels;

namespace FoodFit.Views;

public partial class LoginPage : ContentPage
{
    private readonly userViewModel _userViewModel;
    private readonly LocalDBService _dbService;
	public LoginPage(userViewModel userViewModel,LocalDBService dBService)
	{
		InitializeComponent();
        _userViewModel = userViewModel;
        _dbService = dBService;
        BindingContext = _userViewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(userName.Text) && !string.IsNullOrEmpty(password.Text))
        {
            var user = await _dbService.GetUserByUserName(userName.Text, password.Text);

            if (user != null)
            {
                _userViewModel.UserName = user.UserName;
                _userViewModel.UserEmail = user.Email;
                _userViewModel.UserId = user.UserId;

                _userViewModel.CurrentWeight = user.CurrentWeight;
                _userViewModel.Height = user.height;
                _userViewModel.GoalWeight = user.GoalWeight;

                //var loginData = new Dictionary<string, object>()

                _userViewModel.CalorieIntake = user.CalorieIntake;

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Banana",
                //    Calories = 20,
                //    Carbs = 20,
                //    Protein = 0,
                //    Fat = 5,
                //    Unit = "single"
                //});
                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Chicken Breast",
                //    Calories = 165,
                //    Carbs = 0,
                //    Protein = 31,
                //    Fat = 3,
                //    Unit = "100g"
                //});

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Brown Rice",
                //    Calories = 216,
                //    Carbs = 45,
                //    Protein = 5,
                //    Fat = 2,
                //    Unit = "1 cup"
                //});

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Broccoli",
                //    Calories = 55,
                //    Carbs = 11,
                //    Protein = 4,
                //    Fat = 1,
                //    Unit = "1 cup"
                //});

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Almonds",
                //    Calories = 164,
                //    Carbs = 6,
                //    Protein = 6,
                //    Fat = 14,
                //    Unit = "28g"
                //});

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Salmon",
                //    Calories = 206,
                //    Carbs = 0,
                //    Protein = 22,
                //    Fat = 13,
                //    Unit = "100g"
                //});

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Egg",
                //    Calories = 78,
                //    Carbs = 1,
                //    Protein = 6,
                //    Fat = 5,
                //    Unit = "large"
                //});

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Sweet Potato",
                //    Calories = 103,
                //    Carbs = 24,
                //    Protein = 2,
                //    Fat = 0,
                //    Unit = "medium"
                //});

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Greek Yogurt",
                //    Calories = 100,
                //    Carbs = 6,
                //    Protein = 10,
                //    Fat = 4,
                //    Unit = "170g"
                //});

                //await _dbService.AddFoodItem(new Foods
                //{
                //    FoodName = "Oatmeal",
                //    Calories = 150,
                //    Carbs = 27,
                //    Protein = 5,
                //    Fat = 3,
                //    Unit = "1/2 cup dry"
                //});


                var appShell = (AppShell)Application.Current.MainPage;
                appShell.FlyoutBehavior = FlyoutBehavior.Flyout;
                await Shell.Current.GoToAsync($"//HomePage");
            
            
            }
            else { await DisplayAlert("Login Error", "Username or Password incorrect", "OK"); }
        }
        else
        {
           await DisplayAlert("Login Error", "Username or Password cannot be blank", "OK");
        }
    }
    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignUpPage());

    }
}//new test 