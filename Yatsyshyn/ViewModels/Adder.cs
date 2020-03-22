using System;
using System.Threading.Tasks;
using System.Windows;
using Yatsyshyn.Auxiliary;
using Yatsyshyn.Auxiliary.Managers;
using Yatsyshyn.Auxiliary.Navigation;
using Yatsyshyn.Auxiliary.Other;
using Yatsyshyn.Models;

namespace Yatsyshyn.ViewModels
{
    internal class Adder : TemplateVm
    {
        #region Fields

        private Person _person = StationManager.CurrentPerson;

        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _cancelCommand;

        #endregion

        public Adder()
        {
        }

        #region Properties

        public Person Person
        {
            get => _person;
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> ProceedCommand =>
            _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(
                ProceedImplementation, CanExecuteProceed));

        public RelayCommand<object> CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(
                CancelImplementation));

        #endregion

        private async void ProceedImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            if (await Task.Run(() =>
            {
                try
                {
                    _person.Validate();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                return true;
            }))
            {
                StationManager.DataStorage.AddPerson(_person);
                _person = new Person("", "", "");
                Person = _person;
            }
            LoaderManager.Instance.HideLoader();
        }

        private bool CanExecuteProceed(object obj)
        {
            return !string.IsNullOrWhiteSpace(Person.Email) && !string.IsNullOrWhiteSpace(Person.FirstName) &&
                   !string.IsNullOrWhiteSpace(Person.LastName);
        }

        private static void CancelImplementation(object obj)
        {
            StationManager.DataVm.UpdateInfo();
            NavigationManager.Instance.Navigate(ViewType.DataView);
        }
    }
}