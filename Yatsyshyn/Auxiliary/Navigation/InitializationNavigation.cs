using System;
using Yatsyshyn.Views;

namespace Yatsyshyn.Auxiliary.Navigation
{
    internal class InitializationNavigation : Navigation
    {
        public InitializationNavigation(IContentOwner contentOwner) : base(contentOwner)
        {
        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.DataView:
                    ViewsDictionary.Add(viewType, new Table());
                    break;
                case ViewType.AddPersonView:
                    ViewsDictionary.Add(viewType, new Adder());
                    break;
                case ViewType.EditPersonView:
                    ViewsDictionary.Add(viewType, new Editor());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}