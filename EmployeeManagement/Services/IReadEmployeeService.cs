using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public interface IReadEmployeeService
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(Guid employeeId);
        Task<List<Employee>> SearchEmployeesAsync(string searchBy, string search);
    }
}
