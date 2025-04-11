namespace FoodFit.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
       
	}

	private int counter = 0;
    private void Button_Clicked(object sender, EventArgs e)
    {
        counter++;
        Button button = (Button)sender;
        button.Text = $"Glass water {counter}";

    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new JournalPage());
    }

}