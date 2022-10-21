using EmployeeManagement.API.Controllers;
using EmployeeManagement.API.Models;
using EmployeeManagement.Application.Contracts;
using EmployeeManagement.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        private EmployeeApiController _employeeApiController;
        private Mock<IEmployeeService> _mockEmployeeService;

        [TestInitialize]

        public void TestInitialize()
        {

            _mockEmployeeService = new Mock<IEmployeeService>();
            _employeeApiController = new EmployeeApiController(_mockEmployeeService.Object);
        }

        [TestMethod]
        public void GetEmployeeById_ReturnSuccess()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.GetEmployeeById(1)).Returns(GetDummyEmployeeSuccess());

            //Act
            var result=_employeeApiController.GetEmployeeById(1) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
        }

        [TestMethod]

        public void GetEmployeeById_ReturnNull()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.GetEmployeeById(2)).Returns(GetDummyEmployeeNull());

            //Act
            var result = _employeeApiController.GetEmployeeById(2) as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [TestMethod]

        public void GetEmployeeById_ReturnNegative()
        {
            //Act
            var result = _employeeApiController.GetEmployeeById(-2) as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status400BadRequest);
        }

        [TestMethod]

        public void GetEmployees_ReturnSuccess()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.GetEmployees()).Returns(new List<EmployeeDto>());

            //Act
            var result = _employeeApiController.GetEmployees() as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status200OK);
        }

        [TestMethod]

        public void GetEmployees_ReturnNull()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.GetEmployees()).Returns(()=>null);

            //Act
            var result = _employeeApiController.GetEmployees() as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
            Assert.AreEqual(result.StatusCode, StatusCodes.Status500InternalServerError);
        }

        [TestMethod]

        public void InsertEmployee_ReturnSuccess()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.InsertEmployee(new EmployeeDto())).Returns(true);

            //Act
            var result = _employeeApiController.InsertEmployee(new EmployeeDetailedViewModel()) as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status200OK);
           
        }

        //[TestMethod]

        //public void InsertEmployee_ReturnNull()
        //{
        //    //Arrange
        //    _mockEmployeeService.Setup(m => m.InsertEmployee(new EmployeeDto())).Returns(false);

        //    //Act
        //    var result = _employeeApiController.InsertEmployee(new EmployeeDetailedViewModel()) as ObjectResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        //}

        [TestMethod]

        public void DeleteEmployee_ReturnSuccess()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.DeleteEmployee(6)).Returns(true);

            //Act
            var result = _employeeApiController.DeleteEmployee(6) as OkObjectResult;

            //Assert
            //Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode==StatusCodes.Status200OK);
        }

        [TestMethod]

        public void DeleteEmployee_ReturnNegative()
        {
            //_mockEmployeeService.Setup(m => m.DeleteEmployee(5)).Returns(false);

            //Act
            var result = _employeeApiController.DeleteEmployee(-6) as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode==StatusCodes.Status400BadRequest);
        }

        [TestMethod]

        public void DeleteEmployee_ReturntNull()
        {
            //Arrange
            _mockEmployeeService.Setup(m => m.DeleteEmployee(3)).Returns(false);

            //Act
            var result = _employeeApiController.DeleteEmployee(3) as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == StatusCodes.Status500InternalServerError);
        }


        private EmployeeDto GetDummyEmployeeSuccess()
        {
            return new EmployeeDto();
        }
        private EmployeeDto GetDummyEmployeeNull()
        {
            return null;
        }
    }
}
