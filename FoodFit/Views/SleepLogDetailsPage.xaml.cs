namespace FoodFit.Views;


public partial class SleepLogDetailsPage : ContentPage
{
	public SleepLogDetailsPage()
	{
		InitializeComponent();
	}
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}