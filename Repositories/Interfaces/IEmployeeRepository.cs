using AegisLabs_Employee_ExcelToPdfApp.Models;

namespace AegisLabs_Employee_ExcelToPdfApp.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }

}
