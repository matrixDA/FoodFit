using static SQLite.SQLite3;

namespace FoodFit.Views;


[QueryProperty(nameof(UserName), "userName")]
[QueryProperty(nameof(UserId), "userId")]
[QueryProperty(nameof(UserEmail), "userEmail")]
public partial class HomePage : ContentPage
{
    public string UserName { get; set; }
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    private const double ShakeThreshold = 2.0; 
    private DateTime _lastShakeTime;

    private const double StepThreshold = 0.5; 
    private int _stepCount = 0;

    private double caloriesBurned = 0;
    
    public HomePage()
	{
		InitializeComponent();
        Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        Accelerometer.Start(SensorSpeed.UI);
    }
    private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs args)
    {
        double zAcceleration = args.Reading.Acceleration.Z;

        if (zAcceleration > StepThreshold)
        {
            if ((DateTime.Now - _lastShakeTime).TotalMilliseconds > 500) 
            {
                _lastShakeTime = DateTime.Now;
                Console.WriteLine($"UI Update - X: {args.Reading.Acceleration.X}, Y: {args.Reading.Acceleration.Y}, Z: {args.Reading.Acceleration.Z}");
                _stepCount++;
                UpdateStepCountDisplay();
            }
        }
       
    }

    private void UpdateStepCountDisplay()
    {
        caloriesBurned = _stepCount / 20.0;
        stepCountLabel.Text = $"Steps: {_stepCount}";
        calorieCounts.Text = $"{caloriesBurned:F1} kcal";
    }


    private int counter = 0;
    private void Button_Clicked(object sender, EventArgs e)
    {
        counter++;
        Button button = (Button)sender;
        button.Text = $"Glass {counter}";

    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new JournalPage());
    }

    private void ImageButton_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SleepTracker());

    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SleepTracker());

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}