using System.Windows;
using Yatsyshyn.Auxiliary;
using Yatsyshyn.Auxiliary.Managers;
using Yatsyshyn.Auxiliary.Other;

namespace Yatsyshyn.ViewModels
{
    internal class MainWindow : TemplateVm, ILoaderOwner
    {
        #region Fields

        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        #endregion

        #region Properties

        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }

        #endregion

        internal MainWindow()
        {
            LoaderManager.Instance.Initialize(this);
        }
    }
}