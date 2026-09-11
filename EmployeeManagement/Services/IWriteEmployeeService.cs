using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;

namespace EmployeeManagement.Services
{
    public interface IWriteEmployeeService
    {
        Task<Employee> AddEmployeeAsync(AddEmployeeViewModel employee);
        Task<Employee?> UpdateEmployeeAsync(Guid employeeId, UpdateEmployeeViewModel employee);
        Task<bool> DeleteEmployeeAsync(Guid employeeId);
    }
}
