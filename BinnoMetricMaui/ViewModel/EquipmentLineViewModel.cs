using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinnoMetricMaui.ViewModel
{
    public partial class EquipmentLineViewModel : ObservableObject
    {
        [ObservableProperty]
        private string message = "mvvm линии";
    }
}
