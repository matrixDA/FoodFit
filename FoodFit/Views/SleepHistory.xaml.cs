using FoodFit.ViewModels;

namespace FoodFit.Views;

public partial class SleepHistory : ContentPage
{
	public SleepHistory()
	{
		InitializeComponent();
    }

    private void Lv_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		var selectedSleepLog = e.Item as SleepLog;
        var sleepLogDetails = new SleepTrackViewModel { SelectedSleepHistory = selectedSleepLog};
       
        var sleepLogDetailsPage = new SleepLogDetailsPage();
        sleepLogDetailsPage.BindingContext = sleepLogDetails;
        Navigation.PushAsync( sleepLogDetailsPage);

    }
}