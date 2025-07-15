using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Repositories;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public List<Employee> GetAllEmployees()
        {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch employees.", ex);
            }
        }

        public Employee? GetEmployeeById(int id)
        {
            try
            {
                return _repo.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to fetch employee with ID {id}.", ex);
            }
        }

        public Employee CreateEmployee(Employee employee)
        {
            try
            {
                return _repo.Add(employee);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create employee.", ex);
            }
        }

        public bool UpdateEmployee(int id, Employee employee)
        {
            try
            {
                return _repo.Update(id, employee);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update employee with ID {id}.", ex);
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                return _repo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete employee with ID {id}.", ex);
            }
        }
    }
}
