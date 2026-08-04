using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinnoMetricMaui.ViewModel
{
    public partial class ProductionRecordViewModel : ObservableObject
    {
        [ObservableProperty]
        private string message = "mvvm записи производства";
    }
}
