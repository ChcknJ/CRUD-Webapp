using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IWriteEmployeeService, IReadEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context) => _context = context;


        // Write
        public async Task<Employee> AddEmployeeAsync(AddEmployeeViewModel viewModel)
        {
            DateTime now = DateTime.UtcNow;

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = viewModel.FirstName,
                MiddleName = viewModel.MiddleName,
                LastName = viewModel.LastName,
                EmailAddress = viewModel.EmailAddress,
                Address = viewModel.Address,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Guid employeeId, UpdateEmployeeViewModel viewModel)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null)
            {
                return null;
            }

            employee.FirstName = viewModel.FirstName;
            employee.MiddleName = viewModel.MiddleName;
            employee.LastName = viewModel.LastName;
            employee.EmailAddress = viewModel.EmailAddress;
            employee.Address = viewModel.Address;
            employee.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return employee;

        }

        public async Task<bool> DeleteEmployeeAsync(Guid employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }


        // Read
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();

            return employees;
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee == null)
            {
                return null;
            }

            return employee;
        }

        public async Task<List<Employee>> SearchEmployeesAsync(string searchBy, string? search)
        {
            var query =  _context.Employees.AsQueryable();

            if (string.IsNullOrWhiteSpace(search))
            {
                return await query.ToListAsync();
            }

            query = searchBy switch
            {
                "FirstName" => query.Where(e=>e.FirstName.Contains(search)),
                "LastName" => query.Where(e=>e.LastName.Contains(search)),
                "EmailAddress" => query.Where(e=>e.EmailAddress.Contains(search)),
                "Address" => query.Where(e=>e.Address.Contains(search)),
                _ => query
            };

            return await query.ToListAsync();
        }
    }
}
