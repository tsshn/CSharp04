using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yatsyshyn.Models;
using System.Windows.Forms;
using Yatsyshyn.Auxiliary;
using Yatsyshyn.Auxiliary.Managers;
using Yatsyshyn.Auxiliary.Navigation;
using Yatsyshyn.Auxiliary.Other;

namespace Yatsyshyn.ViewModels
{
    internal class Table : TemplateVm
    {
        public Table()
        {
            StationManager.DataVm = this;
        }

        #region Fields

        private List<Person> _person = StationManager.DataStorage.PersonsList;

        private string[] _sortList = {"Name", "Surname", "Email", "Birthday", "WesternSign", "ChineseSign"},
            _filterList = {"Name", "Surname", "Email", "WesternSign", "ChineseSign"};

        private RelayCommand<object> _addCommand;
        private RelayCommand<object> _editCommand;
        private RelayCommand<object> _refreshCommand;
        private RelayCommand<object> _removeCommand;
        private RelayCommand<object> _filterCommand;

        private int _sortIndex, _filterIndex;

        #endregion

        #region Properties

        public int SortIndex
        {
            get => _sortIndex;
            set
            {
                _sortIndex = value;
                OnPropertyChanged($"PersonList");
            }
        }

        public int FilterIndex
        {
            get => _filterIndex;
            set
            {
                _filterIndex = value;
                OnPropertyChanged($"PersonList");
            }
        }

        public string FilterQuery { get; set; }

        public object SelectedPerson { get; set; }

        public IEnumerable<Person> PersonList
        {
            get
            {
                IEnumerable<Person> list = _person;

                switch (SortIndex)
                {
                    case 0:
                        list = list.OrderBy(x => x.FirstName);
                        break;
                    case 1:
                        list = list.OrderBy(x => x.LastName);
                        break;
                    case 2:
                        list = list.OrderBy(x => x.Email);
                        break;
                    case 3:
                        list = list.OrderBy(x => x.Birthday);
                        break;
                    case 4:
                        list = list.OrderBy(x => x.WesternSign);
                        break;
                    case 5:
                        list = list.OrderBy(x => x.ChineseSign);
                        break;
                }

                if (string.IsNullOrWhiteSpace(FilterQuery))
                    return list;

                switch (FilterIndex)
                {
                    case 0:
                        list = list.Where(x => x.FirstName.Contains(FilterQuery));
                        break;
                    case 1:
                        list = list.Where(x => x.LastName.Contains(FilterQuery));
                        break;
                    case 2:
                        list = list.Where(x => x.Email.Contains(FilterQuery));
                        break;
                    case 3:
                        list = list.Where(x => x.WesternSign.Contains(FilterQuery));
                        break;
                    case 4:

                        list = list.Where(x => x.ChineseSign.Contains(FilterQuery));

                        break;
                }

                return list;
            }
        }

        public IEnumerable<string> SortList => _sortList;

        public IEnumerable<string> FilterList => _filterList;

        public RelayCommand<object> AddPersonCommand =>
            _addCommand ?? (_addCommand = new RelayCommand<object>(
                AddPersonImplementation));

        public RelayCommand<object> EditPersonCommand =>
            _editCommand ?? (_editCommand =
                new RelayCommand<object>(EditPersonImplementation, CanExecuteRemoveOrEdit));

        public RelayCommand<object> RefreshCommand
        {
            get
            {
                return _refreshCommand ?? (_refreshCommand = new RelayCommand<object>(
                    (o => { OnPropertyChanged($"PersonList"); })));
            }
        }

        public RelayCommand<object> RemovePersonCommand =>
            _removeCommand ?? (_removeCommand =
                new RelayCommand<object>(RemovePersonImplementation, CanExecuteRemoveOrEdit));

        public RelayCommand<object> FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new RelayCommand<object>(
                    (o => { OnPropertyChanged($"PersonList"); })));
            }
        }

        #endregion

        private void AddPersonImplementation(object obj)
        {
            StationManager.CurrentPerson = new Person("", "", "");
            NavigationManager.Instance.Navigate(ViewType.AddPersonView);
        }

        private async void RemovePersonImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            if (SelectedPerson == null)
            {
                MessageBox.Show(
                    "Select the record",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                LoaderManager.Instance.HideLoader();
                return;
            }

            await Task.Run(() =>
            {
                var personToRemove = (Person) SelectedPerson;

                DialogResult dr = MessageBox.Show(
                    "Remove " + personToRemove.FirstName + " " + personToRemove.LastName + "?",
                    "Remove",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    StationManager.DataStorage.RemovePerson(personToRemove);
                    OnPropertyChanged($"PersonList");
                }
            });

            LoaderManager.Instance.HideLoader();
        }

        private async void EditPersonImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            if (SelectedPerson == null)
            {
                MessageBox.Show(
                    "Select the record",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                LoaderManager.Instance.HideLoader();
                return;
            }

            await Task.Run(() =>
            {
                StationManager.CurrentPerson = (Person) SelectedPerson;

                StationManager.Temporary = new Person(
                    StationManager.CurrentPerson.FirstName,
                    StationManager.CurrentPerson.LastName,
                    StationManager.CurrentPerson.Email,
                    StationManager.CurrentPerson.Birthday
                );
            });
            LoaderManager.Instance.HideLoader();
            if (StationManager.EditVm != null)
                StationManager.EditVm.UpdateAll();

            NavigationManager.Instance.Navigate(ViewType.EditPersonView);
        }

        private bool CanExecuteRemoveOrEdit(object obj)
        {
            return SelectedPerson != null;
        }

        public void UpdateInfo()
        {
            OnPropertyChanged($"PersonList");
        }
    }
}