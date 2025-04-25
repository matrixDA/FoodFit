using System.Diagnostics;
using FoodFit.Models;
using FoodFit.ViewModels;

namespace FoodFit.Views;

//[QueryProperty(nameof(UserName), "userName")]
//[QueryProperty(nameof(UserId), "userId")]
//[QueryProperty(nameof(UserEmail), "userEmail")]
public partial class HomePage : ContentPage
{
    private readonly userViewModel _userViewModel;
    private readonly LocalDBService _dbService;

  //  public string UserName { get; set; }
 //   public int UserId { get; set; }
 //   public string UserEmail { get; set; }
    private DateTime _lastShakeTime;

    private const double StepThreshold = 0.5;
    private int _stepCount;

    private double caloriesBurned;
    
    public HomePage(userViewModel userViewModel, LocalDBService localDBService)
	{
		InitializeComponent();
        _dbService = localDBService;
        _userViewModel = userViewModel;
        BindingContext = _userViewModel;
        //Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        //Accelerometer.Start(SensorSpeed.UI);
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

    private async void UpdateStepCountDisplay()
    {
        caloriesBurned = _stepCount / 20.0;
        stepCountLabel.Text = $"Steps: {_stepCount}";
        calorieCounts.Text = $"{caloriesBurned:F1} kcal";

        // Update the step count fields on the database every calories burned
        if (_stepCount % 20 == 0)
        {
           
            await _dbService.UpdateStepLogEntry(_userViewModel.UserId, DateTime.Today, caloriesBurned, _stepCount);
        }
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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var data = await _dbService.GetStepCount(_userViewModel.UserId, DateTime.Today);

        if (data != null)
        {
            stepCountLabel.Text = Convert.ToString(data.Steps);
            calorieCounts.Text = $"{Convert.ToString(data.CaloriesBurned)} Kcal";
            caloriesBurned = data.CaloriesBurned;
            _stepCount = Convert.ToInt32(data.Steps);
        }
        else
        {
            await _dbService.CreateStepLogEntry(new StepCount
            {
                UserId = _userViewModel.UserId,
                Steps = 0,
                CaloriesBurned = 0,
                EntryDate = DateTime.Now.Date,
            });
            _stepCount = 0;
            caloriesBurned = 0;
        }

        if (!Accelerometer.IsMonitoring)
        {
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.UI);
        }
    }

    //protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);

    //    var data = await _dbService.GetStepCount(_userViewModel.UserId, DateTime.Now.Date);

    //    if (data != null)
    //    {
    //        stepCountLabel.Text = data.Steps.ToString();
    //        calorieCounts.Text = data.Steps.ToString();
    //        caloriesBurned = data.CaloriesBurned;
    //        _stepCount = Convert.ToInt32(data.Steps);
    //    }
    //    else
    //    {
    //        await _dbService.CreateStepLogEntry(new StepCount
    //        {
    //            UserId = _userViewModel.UserId,
    //            Steps = 0,
    //            CaloriesBurned = 0,
    //            EntryDate = DateTime.Now.Date,
    //        });
    //        _stepCount = 0;
    //        caloriesBurned = 0;
    //    }
    //}
}