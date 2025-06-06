using AegisLabs_Employee_ExcelToPdfApp.Models;
using AegisLabs_Employee_ExcelToPdfApp.Repositories.Interfaces;
using AegisLabs_Employee_ExcelToPdfApp.Services.Interfaces;

namespace AegisLabs_Employee_ExcelToPdfApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Employee>> GetAllAsync() => _repo.GetAllAsync();
        public Task AddAsync(Employee employee) => _repo.AddAsync(employee);
        public Task UpdateAsync(Employee employee) => _repo.UpdateAsync(employee);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);

    }

}
