using FoodFit.ViewModels;

namespace FoodFit.Views
{
<<<<<<< HEAD
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }
    }
}
=======
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
}
>>>>>>> master
