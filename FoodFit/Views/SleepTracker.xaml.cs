using FoodFit.ViewModels;

namespace FoodFit.Views;

public partial class SleepTracker : ContentPage
{
    private SleepHistoryViewModel sleepHistoryViewModel;
    public SleepTracker()
	{
		InitializeComponent();
        sleepHistoryViewModel = new SleepHistoryViewModel();
        BindingContext = sleepHistoryViewModel;

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var sleepHistory = new SleepHistory();
        sleepHistory.BindingContext = sleepHistoryViewModel;
        Navigation.PushAsync(sleepHistory);

    }
}