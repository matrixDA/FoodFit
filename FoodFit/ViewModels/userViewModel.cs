
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace FoodFit.ViewModels
{

    public class userViewModel : INotifyPropertyChanged
    {

        private string _userName;
        private string _userEmail;
        private double _weight;
        private double _height;

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
            get => _weight;
            set
            {
                _weight = value;
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
