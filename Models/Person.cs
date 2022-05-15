using System;
using System.Text.RegularExpressions;
using Task2.Exceptions;

namespace Task2.Models
{
    internal class Person
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;
        private DateTime? _dateOfBirthNullable;
        private readonly string[] _westernSigns = { "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Saggitarius", "Capricorn" };
        private readonly string[] _chineseSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
        private string _sunSign;
        private string _chSign;
        private bool? _isAdult;
        private bool? _isBirthday;

        internal bool? IsAdult
        {
            get
            {
                return _isAdult;
            }
        }
        internal void IsAdultFunc()
        {
            _isAdult = (((DateTime.Today - DateOfBirth).Days / 365) >= 18);
        }

        internal string SunSign
        {
            get { return _sunSign; }
        }

        internal string ChineseSign
        {
            get
            {
                return _chSign;
            }
        }

        internal bool? IsBirthday
        {
            get { return _isBirthday; }
        }

        internal void IsBirthdayFunc()
        {
            _isBirthday = _dateOfBirth.Day == DateTime.Today.Day && _dateOfBirth.Month == DateTime.Today.Month;
        }

        internal string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
            }
        }
        internal string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
            }
        }

        internal string Email
        {
            get { return _email; }
            set
            {
                _email = value;
            }
        }

        internal DateTime? DateOfBirthNullable
        {
            get { return _dateOfBirthNullable; }
            set
            {
                _dateOfBirthNullable = value;
                _dateOfBirth = (System.DateTime)_dateOfBirthNullable;
            }
        }

        internal DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
        }

        internal Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dateOfBirth = dateOfBirth;
          
        }

        internal Person(string firstName, string lastName, string email) : this(firstName, lastName, email, new DateTime())
        { }

        internal Person(string firstName, string lastName, DateTime dateOfBirth) : this(firstName, lastName, null, dateOfBirth)
        { }

        internal Person()
        {
        }

        internal void CalcSunSign()
        {
            int day = _dateOfBirth.Day;
            int month = _dateOfBirth.Month;

            if (month == 1 || month == 4)
                _sunSign = day >= 20 ? _westernSigns[month - 1] : (month == 1 ? _westernSigns[_westernSigns.Length - 1] : _westernSigns[month - 2]);

            if (month == 2)
                _sunSign = day >= 19 ? _westernSigns[month - 1] : _westernSigns[month - 2];

            if (month == 3 || month == 5 || month == 6)
                _sunSign = day >= 22 ? _westernSigns[month - 1] : _westernSigns[month - 2];

            if (month == 7 || month == 8 || month == 9 || month == 10)
                _sunSign = day >= 23 ? _westernSigns[month - 1] : _westernSigns[month - 2];

            _sunSign = day >= 22 ? _westernSigns[month - 1] : _westernSigns[month - 2];
        }

        internal void CalcChSign()
        {
            _chSign = _chineseSigns[(_dateOfBirth.Year - 4) % 12];
        }



        #region Validations
        string _pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                         @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";                 //msdn
        internal void ValidateBirthday()
        {
            if (DateOfBirth > DateTime.Today)
            {
                throw new BirthdayInFutureException("You were born in future! You cannot register unborn people!");
            }
            if (DateTime.Today.Year - DateOfBirth.Year > 135)
            {
                throw new BirthdayInDistantPastException("The age is > 135. You cannot register dead men!");
            }
        }

        internal void ValidateEmail()
        {
            if(!Regex.IsMatch(Email, _pattern, RegexOptions.IgnoreCase))
                throw new InvalidEmailException("Invalid E-mail format!");
        }

        internal void Validate()
        {
            ValidateBirthday();
            ValidateEmail();
        }
        #endregion
    }
}
