namespace FoodFit.Views;
using FoodFit.Models;
using FoodFit.ViewModels;

public partial class ProfilePage : ContentPage
{

    private readonly userViewModel _userViewModel;
    private readonly LocalDBService _dbService;



    public ProfilePage(userViewModel userViewModel, LocalDBService localDBService)
    {
        InitializeComponent();
        _userViewModel = userViewModel;
        BindingContext = _userViewModel;
        _dbService = localDBService;

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UpdateGoalPage(_dbService,_userViewModel, _userViewModel.UserId));
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new UpdateWeight(_dbService, _userViewModel, _userViewModel.UserId));

    }


}