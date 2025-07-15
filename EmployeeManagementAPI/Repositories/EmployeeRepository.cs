using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new();
        private int _nextId = 1;

        public List<Employee> GetAll()
        {
            try
            {
                return _employees;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve employees.", ex);
            }
        }

        public Employee? GetById(int id)
        {
            try
            {
                return _employees.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving employee with ID {id}.", ex);
            }
        }

        public Employee Add(Employee employee)
        {
            try
            {
                employee.Id = _nextId++;
                _employees.Add(employee);
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding employee.", ex);
            }
        }

        public bool Update(int id, Employee updated)
        {
            try
            {
                var emp = GetById(id);
                if (emp == null) return false;

                emp.FullName = updated.FullName;
                emp.Department = updated.Department;
                emp.Salary = updated.Salary;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating employee with ID {id}.", ex);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var emp = GetById(id);
                if (emp == null) return false;

                _employees.Remove(emp);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting employee with ID {id}.", ex);
            }
        }
    }
}
