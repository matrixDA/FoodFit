using FoodFit.Views;

namespace FoodFit;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("HomePage", typeof(HomePage));
	}
}