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
    public partial class EmployeeViewModel : ObservableObject
    {
        private readonly EmployeeService _employeeService;
        public EmployeeViewModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
            // Загружаем сотрудников при инициализации
            _ = LoadEmployeesAsync();
        }
        [ObservableProperty]
        private string logMessage = "";

        [ObservableProperty]
        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

        [RelayCommand]
        public async Task UpdateEmployeeList()
        {
            await LoadEmployeesAsync();
        }
        [RelayCommand]
        public async Task DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (result) logMessage += $"\nСотрудник с ID: {id} удалён";
            else logMessage += "\nНе удалось удалить сотрудника...";
            await LoadEmployeesAsync();
        }

        private async Task LoadEmployeesAsync()
        {
            try
            {
                var employeeList = await _employeeService.GetEmployeeAsync();
                
                Employees.Clear();
                foreach (var employee in employeeList)
                {
                    Employees.Add(employee);
                }
                
                LogMessage += $"\nЗагружено {Employees.Count} сотрудников";
            }
            catch (Exception ex)
            {
                LogMessage += $"\nОшибка загрузки: {ex.Message}";
            }
        }
        [RelayCommand]
        private async Task AddEmployee()
        {
            string name = await Application.Current.MainPage.DisplayPromptAsync(
                "Добавление сотрудника",
                "Введите ФИО сотрудника:",
                "Добавить",
                "Отмена");

            if (!string.IsNullOrWhiteSpace(name))
            {
                var newEmployee = new Employee
                {
                    FullName = name,
                };

                bool result = await _employeeService.AddEmployeeAsync(newEmployee);

                if (result)
                {
                    LogMessage += $"\nСотрудник '{name}' успешно добавлен!";
                    await LoadEmployeesAsync();
                }
                else
                {
                    LogMessage += "\nОшибка при добавлении сотрудника!";
                }
            }
        }
        [RelayCommand]
        private void CLearLogMessage()
        {
            LogMessage = "";
        }
    }
}
