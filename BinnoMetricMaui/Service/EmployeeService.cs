using BinnoMetricMaui.Model;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BinnoMetricMaui.Service;
public class EmployeeService
{
    public readonly HttpClient _httpClient;
    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Employee>> GetEmployeeAsync()
    {
        string url = "https://localhost:7259/api/Employees/GetEmployees";

        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var employees = JsonSerializer.Deserialize<List<Employee>>(response);
            return employees;
        }
        catch
        {
            return new List<Employee>();
        }
    }

    internal async Task<bool> DeleteEmployeeAsync(int id)
    {
        string url = $"https://localhost:7259/api/Employees/DeleteEmployee?id={id}";
        try
        {
            var response = await _httpClient.PutAsync(url, null);
            return true;
        }
        catch
        {
            return false;
        }
    }
}