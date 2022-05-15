using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Task2.Exceptions;
using Task2.Models;
using Task2.Tools;
using Task2.Tools.MVVM;

namespace Task2.ViewModels
{
    internal class UserViewModel : INotifyPropertyChanged, ILoaderOwner
    {

        public event PropertyChangedEventHandler PropertyChanged;
        #region Loader
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;



        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get { return _isControlEnabled; }
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }


        public UserViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }
        #endregion

        private Person _person = new Person();
        private RelayCommand<object> _calcAge;


        #region Properties
        public DateTime? DateOfBirthNullable
        {
            get
            {
                return _person.DateOfBirthNullable;
            }
            set
            {
                _person.DateOfBirthNullable = value;
            }
        }

        public string SunSign
        {
            get { return _person.SunSign; }
        }

        public string ChineseSign
        {
            get { return _person.ChineseSign; }
        }

        public bool? IsBirthday
        {
            get { return _person.IsBirthday; }
        }

        public bool? IsAdult
        {
            get { return _person.IsAdult; }
        }

        public string FirstName
        {
            get { return _person.FirstName; }
            set
            {
                _person.FirstName = value;
            }
        }

        public string LastName
        {
            get { return _person.LastName; }
            set
            {
                _person.LastName = value;
            }
        }

        public string Email
        {
            get { return _person.Email; }
            set
            {
                _person.Email = value;
            }
        }

        #endregion

        private async void ProceedImplementation()
        {

            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => Thread.Sleep(500));
            LoaderManager.Instance.HideLoader();

            try
            {
                _person.Validate();
            }
            catch (BirthdayInFutureException e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            catch (BirthdayInDistantPastException e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            catch (InvalidEmailException e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            await Task.Run(() =>
            {
                _person.CalcSunSign();
                _person.CalcChSign();
                _person.IsBirthdayFunc();
                _person.IsAdultFunc();
            });

            OnPropertyChanged(nameof(DateOfBirthNullable));
            OnPropertyChanged(nameof(IsAdult));
            OnPropertyChanged(nameof(IsBirthday));
            OnPropertyChanged(nameof(SunSign));
            OnPropertyChanged(nameof(ChineseSign));
            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Email));

            // ReSharper disable once PossibleInvalidOperationException
            if ((bool)IsBirthday)
                MessageBox.Show("Congratulations and best wishes on your Birthday!");
        }

        public RelayCommand<object> ProceedCommand
        {
            get
            {
                // ReSharper disable once ConvertToNullCoalescingCompoundAssignment
                return _calcAge ?? (_calcAge = new RelayCommand<object>(o =>
                        ProceedImplementation(), o => CanExecuteCommand()
                ));
            }
        }

        private bool CanExecuteCommand()
        {
            return DateOfBirthNullable != null && !string.IsNullOrWhiteSpace(FirstName)
                                               && !string.IsNullOrWhiteSpace(LastName)
                                               && !string.IsNullOrWhiteSpace(Email);
        }

        #region INotifyImplementation
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
