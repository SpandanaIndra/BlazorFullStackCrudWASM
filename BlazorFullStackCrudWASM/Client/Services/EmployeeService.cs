using BlazorFullStackCrudWASM.Shared;
using System.Net.Http.Json;

namespace BlazorFullStackCrudWASM.Client.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _http;
        public EmployeeService(HttpClient http)
        {
            _http = http;
        }

        public async Task<int> DeleteEmployee(int id)
        {
            var employee = await _http.DeleteFromJsonAsync<Employee>($"api/Employee/DeleteEmployee/{id}");
            if (employee != null)
            {
                return 1;
            }
            else
                return 0;

        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _http.GetFromJsonAsync<Employee>($"api/Employee/GetEmployeeById/{id}");
            return employee;
        }

        //  public List<Employee> employees { get; set; }= new List<Employee>();

        public async Task<List<Employee>> GetEmployees()
        {
            var result = await _http.GetFromJsonAsync<List<Employee>>("api/Employee/GetAllEmployees");
           
                return result;

        }

        public async Task<int> InsertEmployee(Employee employee)
        {
            var result = await _http.PostAsJsonAsync("api/Employee/InsertEmployee",employee);
            if (result != null)
            {
                return 1;
            }
            else
                return 0;
        }

        public async Task<int> UpdateEmployee(int id,Employee employee)
        {
            var result = await _http.PutAsJsonAsync($"api/Employee/UpdateEmployee/{id}",employee);
            if(result != null)
            {
                return 1;
            }
            else
                return 0;
        }
    }
}
