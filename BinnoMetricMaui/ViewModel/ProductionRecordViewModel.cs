using BinnoMetricMaui.Model;
using BinnoMetricMaui.Service;
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
        _ = LoadProductinRecordsAsync();
    }

    [ObservableProperty]
    private string logMessage = "";

    [ObservableProperty]
    private ObservableCollection<ProductionRecord> productionRecords = new ObservableCollection<ProductionRecord>();

    [RelayCommand]
    public async Task UpdatepPoductionRecordsList()
    {
        await LoadProductinRecordsAsync();
    }

    [RelayCommand]
    public async Task DeleteProductinRecord(int id)
    {
        var result = await _productionRecordService.DeleteProductionRecordAsync(id);
        if (result) LogMessage += $"\nПроизводственная запись с ID: {id} удалена";
        else LogMessage += "\nНе удалось удалить производственную запись.";
        await LoadProductinRecordsAsync();
    }

    private async Task LoadProductinRecordsAsync()
    {
        try
        {
            var productionRecordsList = await _productionRecordService.GetProductionRecordAsync();

            ProductionRecords.Clear();
            foreach (var record in productionRecordsList)
            {
                ProductionRecords.Add(record);
            }

            LogMessage += $"\nЗагружено {ProductionRecords.Count} продуктов";
        }
        catch (Exception ex)
        {
            LogMessage += $"\nОшибка загрузки: {ex.Message}";
        }
    }

    [RelayCommand]
    private void CLearLogMessage()
    {
        LogMessage = "";
    }

}
