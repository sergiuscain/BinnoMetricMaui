using CommunityToolkit.Mvvm.ComponentModel;

namespace BinnoMetricMaui.Model;

public partial class PageModel : ObservableObject
{
    private int number;
    private Color color = Color.FromArgb("#1A2332");

    public int Number
    {
        get => number;
        set => SetProperty(ref number, value);
    }

    public Color Color
    {
        get => color;
        set => SetProperty(ref color, value);
    }
}