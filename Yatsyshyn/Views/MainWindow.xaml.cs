using System.Windows.Controls;
using Yatsyshyn.Auxiliary.DataStorage;
using Yatsyshyn.Auxiliary.Managers;
using Yatsyshyn.Auxiliary.Navigation;

namespace Yatsyshyn.Views
{
    public partial class MainWindow : IContentOwner
    {
        public ContentControl ContentControl => Control;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModels.MainWindow();

            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigation(this));
            NavigationManager.Instance.Navigate(ViewType.DataView);
        }
    }
}