﻿using System.Windows.Controls;

 namespace Yatsyshyn.Auxiliary.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}