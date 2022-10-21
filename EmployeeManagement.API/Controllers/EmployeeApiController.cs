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
                ValidateEmployee(employeeId);
                
                var employeeDetailedViewModel = _employeeService.GetEmployeeById(employeeId);
                if (employeeDetailedViewModel == null)
                {
                    throw new Exception("Employee Not Found");
                }

                return Ok(MapToEmployee(employeeDetailedViewModel));
            }
            catch(ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
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
                if (listOfEmployeeViewModel == null)
                {

                    throw new Exception("Employees Not Found");
                }

                return Ok(MapToEmployees(listOfEmployeeViewModel));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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

                //if (insertEmployee==false)
                //{
                //    throw new Exception("Not Insert");
                //}

                return Ok(insertEmployee);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                ValidateEmployee(id);

                var getEmployee = _employeeService.GetEmployeeById(id);
    
                var deleteEmployee = _employeeService.DeleteEmployee(id);
                if (deleteEmployee == false)
                {
                    throw new Exception("Employee is Null");
                }
                return Ok(deleteEmployee);
                 
            }
            catch(ArgumentException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        private void ValidateEmployee(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException("Invalid ID");
            }
        }

       
       
    }
}
