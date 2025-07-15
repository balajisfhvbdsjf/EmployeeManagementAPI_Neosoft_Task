namespace EmployeeManagementAPI.Dtos
{
    public class EmployeeUpdateDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
