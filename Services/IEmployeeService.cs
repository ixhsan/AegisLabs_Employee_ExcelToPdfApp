using AegisLabs_Employee_ExcelToPdfApp.Models;

namespace AegisLabs_Employee_ExcelToPdfApp.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task SaveEmployeesAsync(List<Employee> employees);
        Task<byte[]> GenerateExcelAsync(List<Employee> employees);
        Task<byte[]> GeneratePdfAsync(List<Employee> employees);
    }
}
