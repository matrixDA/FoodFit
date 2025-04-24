
using FoodFit.Models;
namespace FoodFit.Views;

public partial class UserCreationPage : ContentPage
{
    string username;
    string email;
    string password;
    private readonly LocalDBService _dbService;

    public UserCreationPage(string username, string email, string password, LocalDBService dBService)
	{
		InitializeComponent();
        this.username = username;
        this.email = email;
        this.password = password;
        _dbService = dBService;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        double CurrentWeightInLbs = Convert.ToDouble(currentWeight.Text);
        double GoalWeightInLbs = Convert.ToDouble(goalWeight.Text);
        double HeightInCm = Convert.ToDouble(height.Text);

        double CurrentWeightInKg = CurrentWeightInLbs * 0.453592;
        double GoalWeightInKg = GoalWeightInLbs * 0.453592;

        string gender = maleRadionButton.IsChecked ? "male" : "female";

        double minNormalWeight = 18.5 * Math.Pow(HeightInCm / 100, 2); // Height in meters

        // Check if the goal weight is within the normal BMI range
        if (GoalWeightInKg < minNormalWeight)
        {
            await DisplayAlert("Warning",
                $"Your goal weight is not within an healty normal weight for your height range. " +
                "Unable to continue, consider adjusting your goal weight for better health.",
                "OK");
            return;
        }

        var newUser = new User
        {
            UserName = username,
            Email = email,
            PasswordHash = password,
            CurrentWeight = CurrentWeightInLbs,
            GoalWeight = GoalWeightInLbs,
            height = Height,
            CalorieIntake = RecommendedCaloriesIntakeCalc(GoalWeightInKg, HeightInCm, gender)
        };

        await _dbService.CreateUser(newUser);
        await DisplayAlert("Success", "User created successfully!", "OK");
        await Navigation.PopToRootAsync();
    }

    public static double RecommendedCaloriesIntakeCalc(double weight, double height, string gender)
    {
        double bmr;
        if (gender.ToLower() == "male")
        {
            bmr = 66 + (13.75 * weight) + (5 * height);
        }
        else
        {
            bmr = 665 + (9.56 * weight) + (1.85 * height);
        }

        double tdee = bmr * 1.375;

        double calPerDay = tdee - 500;
        return Math.Round(calPerDay, 2);


    }
}