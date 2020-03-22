using Yatsyshyn.Auxiliary.Navigation;

namespace Yatsyshyn.Views
{
    public partial class Editor : INavigatable
    {
        public Editor()
        {
            InitializeComponent();
            DataContext = new ViewModels.Editor();
        }
    }
}