using System.ComponentModel;
using System.Runtime.CompilerServices;
using FoodFit.Models;

namespace FoodFit.ViewModels
{
    public class FoodDetailViewModel : INotifyPropertyChanged
    {
        private double _servings = 1;
        private string _selectedMealType = "Lunch";

        public Foods SelectedFood { get; set; }

        public double Servings
        {
            get => _servings;
            set
            {
                if (_servings != value)
                {
                    if (_servings != value)
                    {
                        _servings = value;
                        OnPropertyChanged();
                        UpdateNutritionalInfo();

                    }
                }
            }
        }

        public string SelectedMealType
        {
            get => _selectedMealType;
            set
            {
                if (_selectedMealType != value)
                {
                    _selectedMealType = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DisplayCalories => $"Calories: {SelectedFood.Calories * Servings:F2}";
        public string DisplayProtein => $"Protein: {SelectedFood.Protein * Servings:F2} g";
        public string DisplayCarbs => $"Carbs: {SelectedFood.Carbs * Servings:F2} g";
        public string DisplayFat => $"Fat: {SelectedFood.Fat * Servings:F2} g";

        public FoodDetailViewModel(Foods selectedFood)
        {
            SelectedFood = selectedFood;
        }

        private void UpdateNutritionalInfo()
        {
            OnPropertyChanged(nameof(DisplayCalories));
            OnPropertyChanged(nameof(DisplayProtein));
            OnPropertyChanged(nameof(DisplayCarbs));
            OnPropertyChanged(nameof(DisplayFat));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
