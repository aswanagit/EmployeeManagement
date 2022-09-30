using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using EmployeeManagement.UI.Providers.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace EmployeeManagement.UI.Providers.ApiClients
{
    public class EmployeeApiClient : IEmployeeApiClient
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployee()
        {
            //Consume /employee endpoint in the EmployeeManagementApi using _httpClient
            using (var response = _httpClient.GetAsync("https://localhost:5001/api/get-all").Result)
            {

                var employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(response.Content.ReadAsStringAsync().Result);
                return employees;
            }
           
        }

        public EmployeeDetailedViewModel GetEmployeeById(int employeeId)
        {
           //Consume /{employeeId} endpoint in the EmployeeManagementApi using _httpClient
          using(var response = _httpClient.GetAsync("https://localhost:5001/api/employee/"+ employeeId).Result)
            {
                
                var employee = JsonConvert.DeserializeObject<EmployeeDetailedViewModel>(response.Content.ReadAsStringAsync().Result);
                return employee;
            }
            
        }
    }
}
