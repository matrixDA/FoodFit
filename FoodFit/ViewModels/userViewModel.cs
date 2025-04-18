﻿
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace FoodFit.ViewModels
{

    public class userViewModel : INotifyPropertyChanged
    {

        private string _userName;
        private string _userEmail;

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
