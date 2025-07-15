using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee? GetById(int id);
        Employee Add(Employee employee);
        bool Update(int id, Employee employee);
        bool Delete(int id);
    }
}
