using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using EmployeeManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet]
        [Route("employee/{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute] int employeeId)
        
        {
            try
            {
                /// get employee by calling GetEmployeeById() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
                var employeeDetailedViewModel = _employeeService.GetEmployeeById(employeeId);

                return Ok(MapToEmployee(employeeDetailedViewModel));
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private EmployeeDetailedViewModel MapToEmployee(EmployeeDto employee)
        {
            var employeeById = new EmployeeDetailedViewModel()
            {
                Id = employee.Id,
                Name=employee.Name,
                Department=employee.Department,
                Age=employee.Age,
                Address=employee.Address

            };
            return employeeById;
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult GetEmployees()
        {
            /// get employees by calling GetEmployees() in IEmployeeService and store it in a variable and Map that variable to EmployeeDetailedViewModel. 
           
            try
            {

                var listOfEmployeeViewModel = _employeeService.GetEmployees();

                return Ok(MapToEmployees(listOfEmployeeViewModel));
            }
            catch(System.Exception)
            {
                throw;
            }
           
        }

        private object MapToEmployees(IEnumerable<EmployeeDto> listOfEmployees)
        {
            var listOfEmployee = new List<EmployeeDetailedViewModel>();
            foreach(var employee in listOfEmployees)
            {
                var employees = new EmployeeDetailedViewModel()
                {
                    Id=employee.Id,
                    Name=employee.Name,
                    Department=employee.Department,
                    Age=employee.Age,
                    Address=employee.Address

                };
                listOfEmployee.Add(employees);
            }
            return listOfEmployee;
        }

        //Create Employee Insert, Update and Delete Endpoint here

        [HttpPost]
        [Route("insert")]
        public IActionResult InsertEmployee([FromBody]EmployeeDetailedViewModel employeeDetail)
        {
            try
            {
               
                var insertEmployee = _employeeService.InsertEmployee(MapToEmployeeInsert(employeeDetail));

                return Ok(insertEmployee);
            }
            catch(System.Exception)
            {
                throw;
            }
          
        }

        private EmployeeDto MapToEmployeeInsert(EmployeeDetailedViewModel insertEmployee)
        {
            var employeeData = new EmployeeDto()
            {
                Name=insertEmployee.Name,
                Department=insertEmployee.Department,
                Age=insertEmployee.Age,
                Address=insertEmployee.Address
            };
            return employeeData;
        }

        [HttpPut]
        [Route("update")]
        public IActionResult UpdateEmployee([FromBody] EmployeeDetailedViewModel employeeData)
        {
            try
            {
                var updateEmployee = _employeeService.UpdateEmployee(MapToUpdate(employeeData));
                if (updateEmployee)
                {
                    return Ok(updateEmployee);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
               
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        private EmployeeDto MapToUpdate(EmployeeDetailedViewModel employeeDetails)
        {
            var employeeDto = new EmployeeDto
            {
                Id=employeeDetails.Id,
                Name=employeeDetails.Name,
                Department=employeeDetails.Department,
                Age=employeeDetails.Age,
                Address=employeeDetails.Address
            };
            return employeeDto;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var deleteEmployee = _employeeService.DeleteEmployee(id);
                return Ok(deleteEmployee);
                 
            }
            catch(System.Exception)
            {
                throw;
            }
        }
       
    }
}
