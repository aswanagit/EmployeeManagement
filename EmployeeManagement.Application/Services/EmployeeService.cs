using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            
            var employees = _employeeRepository.GetEmployeeById(id);
            
            return MapToEmployeeDto(employees);
        }

        private EmployeeDto MapToEmployeeDto(EmployeeData employee)
        {
            var employe = new EmployeeDto()
            {
                Id= employee.Id,
                Name=employee.Name,
                Department=employee.Department,
                Age=employee.Age,
                Address=employee.Address

            };
            return employe;
        }

        public IEnumerable<EmployeeDto> GetEmployees()
        {

            //Get data from Repository
            var employee = _employeeRepository.GetEmployees();
            return MapToEmployeesDto(employee);
        }

        private IEnumerable<EmployeeDto> MapToEmployeesDto(IEnumerable<EmployeeData> listOfEmployees)
        {
            var employeeList = new List<EmployeeDto>();
            
            foreach(var item in listOfEmployees)
            {
                var employe = new EmployeeDto
                {
                    Id = item.Id,
                    Name=item.Name,
                    Department=item.Department,
                    Age=item.Age,
                    Address=item.Address

                };
                employeeList.Add(employe);
            }
            return employeeList;

            
        }

        public bool InsertEmployee(EmployeeDto employeeDto)
        {
           
            var employee = _employeeRepository.InsertEmployee(MapToEmployeeDtos(employeeDto));
            return (employee);
        }

        private EmployeeData MapToEmployeeDtos(EmployeeDto employee)
        {
            var employe = new EmployeeData
            {

                Name = employee.Name,
                Department=employee.Department,
                Age=employee.Age,
                Address=employee.Address
         
            };
            return employe;
        }

        public bool UpdateEmployee(EmployeeDto employeeDto)
        {
            var employee = _employeeRepository.UpdateEmployee(MapToEployeeUpdate(employeeDto));
            return employee;
        }

        private EmployeeData MapToEployeeUpdate(EmployeeDto employee)
        {
            var employeeDtos = new EmployeeData
            {
                Id=employee.Id,
                Name=employee.Name,
                Department=employee.Department,
                Age=employee.Age,
                Address=employee.Address

            };
            return employeeDtos;
        }

        public bool DeleteEmployee(int id)
        {
            var employeee = _employeeRepository.DeleteEmployee(id);
            return employeee;
        }
    }
}
