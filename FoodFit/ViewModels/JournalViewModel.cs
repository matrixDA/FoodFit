using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using FoodFit.Models;

namespace FoodFit.ViewModels
{
    public class JournalViewModel : INotifyPropertyChanged
    {
        private readonly LocalDBService _dbService;
        private DailyNutrition _dailyNutrition;
        private StepCount _stepCount;
        private readonly userViewModel _userViewModel;
        private DateTime _selectedDate = DateTime.Now.Date;
        private ObservableCollection<Grouping<string, FoodLog>> _groupedJournalEntries = new ObservableCollection<Grouping<string, FoodLog>>();

        private double _remainingCalories;
        private Color _remainingCaloriesColor;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged();
                    LoadJournalEntries();
                    LoadDailyData();
                }
            }
        }

        public ObservableCollection<Grouping<string, FoodLog>> GroupedJournalEntries
        {
            get => _groupedJournalEntries;
            set
            {
                _groupedJournalEntries = value;
                OnPropertyChanged();
            }
        }
        public DailyNutrition DailyNutrition
        {
            get => _dailyNutrition;
            set
            {
                _dailyNutrition = value;
                OnPropertyChanged();
                UpdateRemainingCalories();            }
        }
        public StepCount StepCount
        {
            get => _stepCount;
            set
            {
                _stepCount = value;
                OnPropertyChanged();
                UpdateRemainingCalories();            }
        }
        public double RemainingCalories
        {
            get => _remainingCalories;
            private set
            {
                if (_remainingCalories != value)
                {
                    _remainingCalories = value;
                    OnPropertyChanged();
                }
            }
        }
        public double UserCalorieIntake => _userViewModel.CalorieIntake;

        public Color RemainingCaloriesColor
        {
            get => _remainingCaloriesColor;
            private set
            {
                if (_remainingCaloriesColor != value)
                {
                    _remainingCaloriesColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private void UpdateRemainingCalories()
        {
            RemainingCalories = _userViewModel.CalorieIntake - (DailyNutrition?.TotalCalories ?? 0) + (StepCount?.CaloriesBurned ?? 0);

            RemainingCaloriesColor = RemainingCalories < 0 ? Colors.Red : Colors.Green;
        }
        public JournalViewModel(LocalDBService dbService, userViewModel userViewModel)
        {
            _dbService = dbService;
            _userViewModel = userViewModel;
            LoadJournalEntries();
            LoadDailyData();
        }

        private async void LoadJournalEntries()
        {
            var userId = _userViewModel.UserId;
            var allEntries = await _dbService.GetFoodLogsForDateAndMeal(userId, SelectedDate);
            var groupedEntries = allEntries
                .GroupBy(entry => entry.MealType.ToString()) 
                .Select(group => new Grouping<string, FoodLog>(group.Key, group.ToList()))
                .ToList();

            GroupedJournalEntries = new ObservableCollection<Grouping<string, FoodLog>>(groupedEntries);
        }
        private async void LoadDailyData()
        {
            var userId = _userViewModel.UserId;

            // Load DailyNutrition for the selected date
            DailyNutrition = await _dbService.GetDailyNutrition(userId, SelectedDate) ?? new DailyNutrition
            {
                TotalCalories = 0,
                TotalProtein = 0,
                TotalCarbs = 0,
                TotalFats = 0
            };

            // Load StepCount for the selected date
            StepCount = await _dbService.GetStepCount(userId, SelectedDate) ?? new StepCount
            {
                CaloriesBurned = 0
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Name { get; }

        public Grouping(K name, IEnumerable<T> items) : base(items)
        {
            Name = name;
        }
    }
}

