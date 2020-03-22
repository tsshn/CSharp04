using Yatsyshyn.Auxiliary.Navigation;

namespace Yatsyshyn.Views
{
    public partial class Adder : INavigatable
    {
        public Adder()
        {
            InitializeComponent();
            DataContext = new ViewModels.Adder();
        }
    }
}