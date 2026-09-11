using EmployeeManagement.Models.ViewModels;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IWriteEmployeeService _writeEmployeeService;
        private readonly IReadEmployeeService _readEmployeeService;

        public EmployeeController(IWriteEmployeeService write, IReadEmployeeService read)
        {
            _writeEmployeeService = write;
            _readEmployeeService = read;
        }



        // Home
        public async Task<IActionResult> Index(string searchBy, string? search)
        {
            List<Employee> employees;

            if (string.IsNullOrWhiteSpace(search))
            {
                employees = await _readEmployeeService.GetEmployeesAsync();
            }
            else
            {
                employees = await _readEmployeeService.SearchEmployeesAsync(searchBy, search);
            }

            return View(employees);
        }


        // Add/Create Employee
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddEmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _writeEmployeeService.AddEmployeeAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }


        // Edit Employee
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _readEmployeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = new UpdateEmployeeViewModel
            {
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                EmailAddress = employee.EmailAddress,
                Address = employee.Address
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            Guid id,
            UpdateEmployeeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var employee = await _writeEmployeeService.UpdateEmployeeAsync(id, viewModel);

            if (employee == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }



        // View Details
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _readEmployeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }



        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _writeEmployeeService.DeleteEmployeeAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
