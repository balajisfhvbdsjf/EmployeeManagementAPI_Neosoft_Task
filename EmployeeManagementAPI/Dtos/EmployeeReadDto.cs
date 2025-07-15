namespace EmployeeManagementAPI.Dtos
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
