namespace FoodFit.Views;

public partial class UserCreationPage : ContentPage
{
	public UserCreationPage()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {

		Navigation.PushAsync(new HomePage());
    }
}