using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FoodFit.Models;
using CommunityToolkit.Mvvm.Input;

namespace FoodFit.Models.ViewModels
{
    public partial class FoodLogViewModel:ObservableObject
    {
        private readonly LocalDBService _localDBService;

        [ObservableProperty]
        private int _userId;

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Now.Date;

        [ObservableProperty]
        private MealType _selectedMealType;

        [ObservableProperty]
        private string _foodName;

        [ObservableProperty]
        private double _quantity;

        [ObservableProperty]
        private string _unit;

        [ObservableProperty]
        private double _calories;

        [ObservableProperty]
        public double _carbs;

        [ObservableProperty]
        private double _protein;

        [ObservableProperty]
        private double _fat;

        public ObservableCollection<FoodLog> FoodLogs { get; } = new ObservableCollection<FoodLog>();

        public FoodLogViewModel(LocalDBService localDBService)
        {
            _localDBService = localDBService;

            UserId = UserId;
        }

        [RelayCommand]
        private async Task AddFood()
        {
            if (string.IsNullOrEmpty(FoodName) || Quantity <= 0)
            {

            }

            var newFoodLog = new FoodLog
            {
                UserId = UserId,
                EntryDate = SelectedDate,
                FoodName = FoodName,
                Quantity = Quantity,
                Unit = Unit,
                Caloroies = Calories,
                Protein = Protein,
                Carbs = Carbs,
                Fat = Fat,
                MealType = SelectedMealType,

            };

            await _localDBService.CreateFoodLogEntry(newFoodLog);

            FoodName = string.Empty;
            Quantity = 0;
            Unit = string.Empty;
            Calories = 0;
            Protein = 0;
            Carbs = 0;
            Fat = 0;

            await LoadFoodLogsCommand.ExecuteAsync(null);
        }

        [RelayCommand]
        private async Task LoadFoodLogs()
        {
            if (UserId > 0)
            {
                FoodLogs.Clear();
                var logs = await Task.Run(() => _localDBService.GetFoodLogsForDMT(UserId, SelectedDate, SelectedMealType));
                foreach (var log in logs)
                {
                    FoodLogs.Add(log);
                }
            }
        }

        public static Array MealTypeValues => Enum.GetValues(typeof(MealType));
    }
}
