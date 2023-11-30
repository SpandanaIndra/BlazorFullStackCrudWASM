using BlazorFullStackCrudWASM.Shared;

namespace BlazorFullStackCrudWASM.Client.Services
{
    public interface IEmployeeService
    {
      
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<int> InsertEmployee(Employee employee);
        Task<int> UpdateEmployee(int id,Employee employee);
        Task<int> DeleteEmployee(int id);

    }
}
