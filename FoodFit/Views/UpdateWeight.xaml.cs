using System.Diagnostics;
using FoodFit.Models;
using FoodFit.ViewModels;

namespace FoodFit.Views;


public partial class UpdateWeight : ContentPage
{
    private readonly LocalDBService _dbService;
    private readonly int userID;
    private readonly userViewModel _userViewModel;
    public UpdateWeight(LocalDBService localDBService, userViewModel userViewModel, int id)
    {
        InitializeComponent();
        _dbService = localDBService;
        _userViewModel = userViewModel;
        BindingContext = _userViewModel;
        userID = id;
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await _dbService.UpdateUserWeight(userID, Convert.ToDouble(updateWeight.Text));



        var userTest = await _dbService.GetUserById(userID);
        Debug.WriteLine($"changed weight: {userTest.CurrentWeight}");

        _userViewModel.CurrentWeight = userTest.CurrentWeight;

    }
}
