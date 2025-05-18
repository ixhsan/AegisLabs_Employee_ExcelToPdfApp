using AegisLabs_Employee_ExcelToPdfApp.Models;

namespace AegisLabs_Employee_ExcelToPdfApp.Services.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> GenerateExcelAsync(List<Employee> employees);
        Task<byte[]> GeneratePdfAsync(List<Employee> employees);
    }
}
