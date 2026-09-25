using BinnoMetricMaui.Model;
using BinnoMetricMaui.Service;
using BinnoMetricMaui.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace BinnoMetricMaui.ViewModel;

public partial class ProductionRecordViewModel : ObservableObject
{
    private readonly ProductionRecordService _productionRecordService;
    public ProductionRecordViewModel(ProductionRecordService productionRecordService)
    {
        _productionRecordService = productionRecordService;
        // Загружаем производственные записи при инициализации
        _ = GetTotalPageCountAsync();
        _ = LoadProductinRecordsAsync();
    }
    // Элементы фильтрации (UI-поля)
    [ObservableProperty] 
    private string equipmentLineIdText = "";
    [ObservableProperty] 
    private string employeeIdText = "";
    [ObservableProperty] 
    private string productIdText = "";
    [ObservableProperty] 
    private string actualQuantityText = "";
    [ObservableProperty] 
    private string seriesNumberText = "";
    [ObservableProperty] 
    private string commentsText = "";

    [ObservableProperty] 
    private bool startTimeEnabled = false;
    [ObservableProperty] 
    private DateTime startTimeValue = DateTime.Today.AddMonths(-1);

    [ObservableProperty] 
    private bool endTimeEnabled = false;
    [ObservableProperty] 
    private DateTime endTimeValue = DateTime.Today;


    [ObservableProperty]
    private string logMessage = "";
    [ObservableProperty]
    private int totalPageCount = 1;
    [ObservableProperty]
    private int currentPageNumber = 1;

    [ObservableProperty]
    private ProductionRecordFilter filter = new ProductionRecordFilter { Page = 0, PageSize = 30};

    [ObservableProperty]
    private ObservableCollection<ProductionRecord> productionRecords = new ObservableCollection<ProductionRecord>();
    [ObservableProperty]
    private ObservableCollection<PageModel> pages = new ObservableCollection<PageModel>();

    private static int? ParseNullableInt(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;
        return int.TryParse(s.Trim(), out var v) ? v : null;
    }
    [RelayCommand]
    public async Task DeleteProductinRecord(int id)
    {
        var result = await _productionRecordService.DeleteProductionRecordAsync(id);
        if (result) LogMessage += $"\nПроизводственная запись с ID: {id} удалена";
        else LogMessage += "\nНе удалось удалить производственную запись.";
        await GetTotalPageCountAsync();
        await LoadProductinRecordsAsync();
    }

    private async Task LoadProductinRecordsAsync()
    {
        try
        {

            var productionRecordsList = await _productionRecordService.GetProductionRecordsAsync(Filter);

            ProductionRecords.Clear();
            foreach (var record in productionRecordsList)
            {
                ProductionRecords.Add(record);
            }

            // Обновляем цвет текущей страницы
            foreach (var page in Pages)
            {
                page.Color = page.Number == CurrentPageNumber ? Color.FromArgb("#2979FF") : Color.FromArgb("#1A2332");
            }

            LogMessage += $"\nЗагружено {ProductionRecords.Count} продуктов";
        }
        catch (Exception ex)
        {
            LogMessage += $"\nОшибка загрузки: {ex.Message}";
        }
    }
    private async Task GetTotalPageCountAsync()
    {
        try
        {
            var result = await _productionRecordService.GetPageCountAsync(Filter);
            TotalPageCount = result;

            Pages.Clear();
            for (int i = 1; i <= TotalPageCount; i++)
            {
                Pages.Add(new PageModel { Number = i });
            }
        }
        catch (Exception ex)
        {
            LogMessage += $"\nОшибка при получении количества страниц: {ex.Message}";
        }
    }
    [RelayCommand]
    private void GoToPage(int pageNumber)
    {
        foreach (var page in Pages)
        {
            page.Color = page.Number == pageNumber ? Color.FromArgb("#2979FF") : Color.FromArgb("#1A2332");
        }

        CurrentPageNumber = pageNumber;
        Filter.Page = pageNumber - 1;
        _ = LoadProductinRecordsAsync();
    }
    [RelayCommand]
    private async Task GoToCharts()
    {
        var chartFilter = new ProductionRecordFilter
        {
            Page = 0,
            PageSize = int.MaxValue,
            EquipmentLineId = Filter.EquipmentLineId,
            EmployeeId = Filter.EmployeeId,
            ProductId = Filter.ProductId,
            ActualQuantity = Filter.ActualQuantity,
            SeriesNumber = Filter.SeriesNumber,
            Comments = Filter.Comments,
            StartTime = Filter.StartTime,
            EndTime = Filter.EndTime,
        };

        var allRecords = await _productionRecordService.GetProductionRecordsAsync(chartFilter);

        var vm = new ProductionRecordsChartsViewModel(allRecords);
        var page = new ProductionRecordsChartsPage(vm);
        await Shell.Current.Navigation.PushAsync(page);
    }
    [RelayCommand]
    private async Task GoToProductionRecordCardPage(int id)
    {
        var productionRecord = await _productionRecordService.GetProductionRecordAsync(id);
        var vm = new ProductionRecordCardViewModel(productionRecord);
        var page = new ProductionRecordCardPage(vm);
        await Shell.Current.Navigation.PushAsync(page);
    }

    [RelayCommand]
    private void CLearLogMessage()
    {
        LogMessage = "";
    }

    [RelayCommand]
    private async Task ApplyFilter()
    {
        Filter.Page = 0;
        Filter.EquipmentLineId = ParseNullableInt(EquipmentLineIdText);
        Filter.EmployeeId = ParseNullableInt(EmployeeIdText);
        Filter.ProductId = ParseNullableInt(ProductIdText);
        Filter.ActualQuantity = ParseNullableInt(ActualQuantityText);
        Filter.SeriesNumber = string.IsNullOrWhiteSpace(SeriesNumberText) ? null : SeriesNumberText.Trim();
        Filter.Comments = string.IsNullOrWhiteSpace(CommentsText) ? null : CommentsText.Trim();
        Filter.StartTime = StartTimeEnabled ? StartTimeValue : DateTime.MinValue;
        Filter.EndTime = EndTimeEnabled ? EndTimeValue.AddDays(1).AddTicks(-1) : DateTime.MaxValue;
        CurrentPageNumber = 1;

        await GetTotalPageCountAsync();
        await LoadProductinRecordsAsync();
    }

}