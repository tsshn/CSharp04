using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Yatsyshyn.Auxiliary;
using Yatsyshyn.Auxiliary.Managers;
using Yatsyshyn.Auxiliary.Navigation;
using Yatsyshyn.Auxiliary.Other;
using Yatsyshyn.Models;

namespace Yatsyshyn.ViewModels
{
    internal class Editor : TemplateVm
    {
        public Editor()
        {
            StationManager.EditVm = this;
        }

        #region Fields

        private Person _person = StationManager.CurrentPerson;

        private Person _temp = StationManager.Temporary;

        private RelayCommand<object> _confirmCommand;
        private RelayCommand<object> _cancelCommand;

        #endregion

        #region Properties

        public Person Person
        {
            get => _temp;
            set
            {
                _temp = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<object> ConfirmCommand =>
            _confirmCommand ?? (_confirmCommand = new RelayCommand<object>(
                ConfirmImplementation, CanExecuteProceed));

        public RelayCommand<object> CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(
                CancelImplementation));

        #endregion

        private async void ConfirmImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => Thread.Sleep(300));

            if (await Task.Run(() =>
            {
                try
                {
                    _temp.Validate();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                return true;
            }))
            {
                _person.FirstName = _temp.FirstName;
                _person.LastName = _temp.LastName;
                _person.Email = _temp.Email;
                _person.Birthday = _temp.Birthday;
                StationManager.Temporary = null;
                StationManager.DataStorage.ApplyChanges();
                StationManager.DataVm.UpdateInfo();
                NavigationManager.Instance.Navigate(ViewType.DataView);
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
            NavigationManager.Instance.Navigate(ViewType.DataView);
        }

        public void UpdateAll()
        {
            Person = StationManager.Temporary;
            _person = StationManager.CurrentPerson;
            OnPropertyChanged($"TestPerson");
        }
    }
}