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
        _ = GetTotalPageCount();
        _ = LoadProductinRecordsAsync();
    }

    [ObservableProperty]
    private string logMessage = "";
    [ObservableProperty]
    private int totalPageCount = 1;
    [ObservableProperty]
    private int currentPageNumber = 1;
    [ObservableProperty]
    private int page = 0;
    [ObservableProperty]
    private int pageSize = 30;

    [ObservableProperty]
    private ObservableCollection<ProductionRecord> productionRecords = new ObservableCollection<ProductionRecord>();
    [ObservableProperty]
    private ObservableCollection<PageModel> pages = new ObservableCollection<PageModel>();

    [RelayCommand]
    public async Task UpdatepPoductionRecordsList()
    {
        await GetTotalPageCount(); 
        await LoadProductinRecordsAsync(); 
    }

    [RelayCommand]
    public async Task DeleteProductinRecord(int id)
    {
        var result = await _productionRecordService.DeleteProductionRecordAsync(id);
        if (result) LogMessage += $"\nПроизводственная запись с ID: {id} удалена";
        else LogMessage += "\nНе удалось удалить производственную запись.";
        await GetTotalPageCount(); 
        await LoadProductinRecordsAsync();
    }

    private async Task LoadProductinRecordsAsync()
    {
        try
        {
            var productionRecordsList = await _productionRecordService.GetProductionRecordAsync(Page, PageSize);

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
    private async Task GetTotalPageCount()
    {
        var result = await _productionRecordService.GetPageCountAsync(PageSize);
        TotalPageCount = result;

        Pages.Clear();
        for (int i = 1; i <= TotalPageCount; i++)
        {
            Pages.Add(new PageModel { Number = i });
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
        Page = pageNumber - 1;
        _ = LoadProductinRecordsAsync();
    }
    [RelayCommand]
    private void GoToProductionRecordCardPage(int id)
    {
        var vm = new ProductionRecordCardViewModel();
        var page = new ProductionRecordCardPage(vm);
        Shell.Current.Navigation.PushAsync(page);
    }

    [RelayCommand]
    private void CLearLogMessage()
    {
        LogMessage = "";
    }

}
