namespace FoodFit.Views;


[QueryProperty(nameof(UserName), "userName")]
[QueryProperty(nameof(UserId), "userId")]
[QueryProperty(nameof(UserEmail), "userEmail")]
public partial class HomePage : ContentPage
{
    public string UserName { get; set; }
    public string UserId { get; set; }
    public string UserEmail { get; set; }
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

    private void ImageButton_Clicked(object sender, EventArgs e)
    {

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}