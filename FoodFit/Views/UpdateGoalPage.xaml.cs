using System.Diagnostics;
using FoodFit.Models;
using FoodFit.ViewModels;

namespace FoodFit.Views;


public partial class UpdateGoalPage : ContentPage
{
    private readonly LocalDBService _dbService;
    private readonly int userID;
    private readonly userViewModel _userViewModel;
    public UpdateGoalPage(LocalDBService localDBService, userViewModel userViewModel, int id)
	{
		InitializeComponent();
        _dbService = localDBService;
        _userViewModel = userViewModel;
        BindingContext = _userViewModel;
        userID = id;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await _dbService.UpdateUser(userID, Convert.ToDouble(goalWeight.Text));



        var userTest = await _dbService.GetUserById(userID);
        Debug.WriteLine($"changed weight: {userTest.GoalWeight}");

        _userViewModel.GoalWeight = userTest.GoalWeight;

    }
}