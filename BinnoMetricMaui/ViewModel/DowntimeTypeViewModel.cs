using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BinnoMetricMaui.ViewModel
{
    public partial class DowntimeTypeViewModel : ObservableObject
    {
        [ObservableProperty]
        private string message = "mvvm Типы простоев";
    }
}
