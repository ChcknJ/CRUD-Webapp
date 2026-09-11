namespace EmployeeManagement.Models.ViewModels
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
        public required string Address { get; set; }
    }
}
