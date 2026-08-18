using BinnoMetricMaui.Model;
using System.Text;
using System.Text.Json;

namespace BinnoMetricMaui.Service;
public class EmployeeService
{
    private readonly HttpClient _httpClient;
    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Employee>> GetEmployeesAsync()
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
    public async Task<Employee> GetEmployeeAsync(int? id)
    {
        string url = $"https://localhost:7259/api/Employees/GetEmployee?id={id}";

        try
        {
            var response = await _httpClient.GetStringAsync(url);
            var employee = JsonSerializer.Deserialize<Employee>(response);
            return employee;
        }
        catch
        {
            return null;
        }
    }

    internal async Task<bool> AddEmployeeAsync(Employee newEmployee)
    {
        string url = "https://localhost:7259/api/Employees/AddEmployee";

        try
        {
            var json = JsonSerializer.Serialize(newEmployee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
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