using Yatsyshyn.Auxiliary.Managers;
using Yatsyshyn.Auxiliary.Navigation;

namespace Yatsyshyn.Views
{
    public partial class Table : INavigatable
    {

        public Table()
        {
            InitializeComponent();
            DataContext = new ViewModels.Table();
            StationManager.PersonTable = Grid;
        }

    }
}