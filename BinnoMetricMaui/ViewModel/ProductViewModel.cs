using BinnoMetricMaui.Model;
using BinnoMetricMaui.Service;
using BinnoMetricMaui.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BinnoMetricMaui.ViewModel
{
    public partial class ProductViewModel : ObservableObject
    {
        private readonly ProductService _productService;
        private readonly AnalyticsService _analyticsService;
        public ProductViewModel(ProductService productService, AnalyticsService analyticsService    )
        {
            _productService = productService;
            _analyticsService = analyticsService;
            // Загружаем продукты при инициализации
            _ = LoadProductsAsync();
        }

        [ObservableProperty]
        private string logMessage = "";

        [ObservableProperty]
        private ObservableCollection<Product> products = new ObservableCollection<Product>();

        [RelayCommand]
        public async Task UpdateProductsList()
        {
            await LoadProductsAsync();
        }
        [RelayCommand]
        public async Task DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (result) LogMessage += $"\nПродукт с ID: {id} удалён";
            else LogMessage += "\nНе удалось удалить продукт. Возможно, этот продукт связан с производственными записями\nУдалите связанные производственные записи для удаления продукта";
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                var productsList = await _productService.GetProductsAsync();

                Products.Clear();
                foreach (var product in productsList)
                {
                    Products.Add(product);
                }

                LogMessage += $"\nЗагружено {Products.Count} продуктов";
            }
            catch (Exception ex)
            {
                LogMessage += $"\nОшибка загрузки: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task AddProduct()
        {
            string name = await Application.Current.MainPage.DisplayPromptAsync(
                "Добавление продукта",
                "Введите название продукта:",
                "Добавить",
                "Отмена");

            if (!string.IsNullOrWhiteSpace(name))
            {
                var newProduct = new Product
                {
                    Name = name,
                };

                bool result = await _productService.AddProductAsync(newProduct);

                if (result)
                {
                    LogMessage += $"\nПродукт '{name}' успешно добавлен!";
                    await LoadProductsAsync();
                }
                else
                {
                    LogMessage += "\nОшибка при добавлении продукта!";
                }
            }
        }

        [RelayCommand]
        private void GetTopEmployeesByProduct(int productId)
        {
            var vm = new TopEmployeesByProductViewModel(productId, _analyticsService);
            var page = new TopEmployeesByProductPage(vm);
            Shell.Current.Navigation.PushAsync(page);
        }

        [RelayCommand]
        private void CLearLogMessage()
        {
            LogMessage = "";
        }
    }
}
