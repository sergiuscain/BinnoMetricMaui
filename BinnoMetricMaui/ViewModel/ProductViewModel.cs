using BinnoMetricMaui.Model;
using BinnoMetricMaui.Service;
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
        public ProductViewModel(ProductService productService)
        {
            _productService = productService;
            // Загружаем продукты при инициализации
            _ = LoadProductsAsync();
        }

        [ObservableProperty]
        private string message = "";

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
            if (result) message += $"\nПродукт с ID: {id} удалён";
            else message += "\nНе удалось удалить продукт. Возможно, этот продукт связан с производственными записями\nУдалите связанные производственные записи для удаления продукта";
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

                Message += $"\nЗагружено {Products.Count} продуктов";
            }
            catch (Exception ex)
            {
                Message += $"\nОшибка загрузки: {ex.Message}";
            }
        }
    }
}
