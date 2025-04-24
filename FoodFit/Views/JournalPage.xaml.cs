using FoodFit.ViewModels;

namespace FoodFit.Views;

public partial class JournalPage : ContentPage
{
    public JournalPage(LocalDBService dbService, userViewModel userViewModel)
    {
        InitializeComponent();
        BindingContext = new JournalViewModel(dbService, userViewModel);
    }
}

