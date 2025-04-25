
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace FoodFit.ViewModels
{

    public class userViewModel : INotifyPropertyChanged
    {

        private string _userName;
        private string _userEmail;
        private int _userId;

        private double _height;
        private double _currentWeight;
        private double _goalWeight;



        private double _calorieIntake;


        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }


        public string UserEmail
        {
            get => _userEmail;
            set
            {
                _userEmail = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public double CurrentWeight
        {
            get => _currentWeight;
            set
            {
                _currentWeight = value;
                OnPropertyChanged();
            }
        }

        public double GoalWeight
        {
            get => _goalWeight;
            set
            {
                _goalWeight = value;
                OnPropertyChanged();
            }
        }

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged();
            }
        }

        public double CalorieIntake
        {
            get => _calorieIntake;
            set
            {
                _calorieIntake = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
