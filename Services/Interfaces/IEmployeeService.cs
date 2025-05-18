using AegisLabs_Employee_ExcelToPdfApp.Models;

namespace AegisLabs_Employee_ExcelToPdfApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task AddAsync(Employee emp);
        Task UpdateAsync(Employee emp);
        Task DeleteAsync(int id);
    }


}
