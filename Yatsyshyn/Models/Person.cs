using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System;
using Yatsyshyn.Auxiliary.Exceptions;

namespace Yatsyshyn.Models
{
    [Serializable]
    internal class Person
    {
        #region Fields

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthday;

        private int _age = -1;
        private string _westernSign;
        private string _chineseSign;
        private string _birthdayString;

        #endregion

        #region Properties

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = Convert.ToDateTime(value);
                _birthdayString = value.ToShortDateString();
                _age = CalcAge();
                _westernSign = CalcWesternHoroscope();
                _chineseSign = CalcChineseHoroscope();

                OnPropertyChanged();
                OnPropertyChanged($"IsAdult");
                OnPropertyChanged($"IsBirthday");
                OnPropertyChanged($"BirthdayString");
                OnPropertyChanged($"WesternSign");
                OnPropertyChanged($"ChineseSign");
            }
        }

        #endregion

        #region ReadOnlyProps

        private int Age => (_age == -1) ? (_age = CalcAge()) : _age;

        public bool IsAdult => (_age != -1) ? 18 <= _age : 18 <= (_age = CalcAge());

        public string BirthdayString => _birthdayString ?? (_birthday.ToString("yyyy MMMM dd"));
        public string WesternSign => _westernSign ?? (_westernSign = CalcWesternHoroscope());

        public string ChineseSign => _chineseSign ?? (_chineseSign = CalcChineseHoroscope());

        public bool IsBirthday => (_birthday.Day == DateTime.Today.Day) && (_birthday.Month == DateTime.Today.Month);

        #endregion

        public void Validate()
        {
            if (DateTime.Today < Birthday.Date) throw new UnbornExc();
            if (Age > 135) throw new TooOldExc();
            var regex = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            if (!regex.IsMatch(Email)) throw new EmailExc(Email);
        }

        private int CalcAge()
        {
            return (DateTime.Today - Convert.ToDateTime(Birthday)).Days / 365;
        }

        private readonly string[] _chineseSigns =
        {
            "Rat 🐀", "Ox 🐂", "Tiger 🐅", "Rabbit 🐇", "Dragon 🐉", "Snake 🐍", "Horse 🐎", "Goat 🐐", "Monkey 🐒",
            "Rooster 🐓", "Dog 🐕", "Pig 🐖"
        };

        private readonly string[] _westernSigns =
        {
            "Aquarius ♒", "Pisces ♓", "Aries ♈", "Taurus ♉", "Gemini ♊", "Cancer ♋", "Leo ♌", "Virgo ♍", "Libra ♍",
            "Scorpio ♏",
            "Sagittarius ♐", "Capricorn ♑"
        };

        private string CalcChineseHoroscope()
        {
            int? index = Birthday.Year - 1900;
            if (index < 0) index *= -1;
            index %= 12;
            return _chineseSigns[(int) index];
        }

        private string CalcWesternHoroscope()
        {
            var month = Birthday.Month;
            var day = Birthday.Day;
            switch (month)
            {
                case 01 when day >= 20:
                case 02 when day <= 18:
                    return _westernSigns[0];
                case 02 when day >= 19:
                case 03 when day <= 20:
                    return _westernSigns[1];
                case 03 when day >= 21:
                case 04 when day <= 19:
                    return _westernSigns[2];
                case 04 when day >= 20:
                case 05 when day <= 20:
                    return _westernSigns[3];
                case 05 when day >= 21:
                case 06 when day <= 20:
                    return _westernSigns[4];
                case 06 when day >= 21:
                case 07 when day <= 22:
                    return _westernSigns[5];
                case 07 when day >= 23:
                case 08 when day <= 22:
                    return _westernSigns[6];
                case 08 when day >= 23:
                case 09 when day <= 22:
                    return _westernSigns[7];
                case 09 when day >= 23:
                case 10 when day <= 22:
                    return _westernSigns[8];
                case 10 when day >= 23:
                case 11 when day <= 21:
                    return _westernSigns[9];
                case 11 when day >= 22:
                case 12 when day <= 21:
                    return _westernSigns[10];
                case 12 when day >= 22:
                case 01 when day <= 19:
                    return _westernSigns[11];
                default: return "";
            }
        }

        #region Constructors

        public Person(string firstName, string lastName, string email, DateTime birthday)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthday = birthday;
        }

        public Person() : this("", "", "", DateTime.Today)
        {
        }

        public Person(string firstName, string lastName, DateTime birthday) : this(firstName, lastName, "", birthday)
        {
        }

        public Person(string firstName, string lastName, string email) : this(firstName, lastName, email,
            DateTime.Today)
        {
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}