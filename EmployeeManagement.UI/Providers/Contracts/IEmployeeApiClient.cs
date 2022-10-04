using EmployeeManagement.UI.Models;
using EmployeeManagement.UI.Models.Provider;
using System.Collections.Generic;

namespace EmployeeManagement.UI.Providers.Contracts
{
    public interface IEmployeeApiClient
    {
        IEnumerable<EmployeeViewModel> GetAllEmployee();
        EmployeeDetailedViewModel GetEmployeeById(int id);
        bool UpdateEmployee(EmployeeDetailedViewModel employee);
        bool InsertEmployee(EmployeeDetailedViewModel employeeDetailedViewModel);
        bool DeleteEmployee(int employeeId);
    }
}
