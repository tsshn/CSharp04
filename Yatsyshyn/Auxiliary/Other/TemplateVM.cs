using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Yatsyshyn.Auxiliary.Other
{
    internal abstract class TemplateVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}