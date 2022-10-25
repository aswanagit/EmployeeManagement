using EmployeeManagement.DataAccess.Contracts;
using EmployeeManagement.DataAccess.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.DataAccess.Repository
{
    /// <summary>
    /// Connect To Database and Perforum CRUD operations
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConnection _sqlConnection;
        
        public EmployeeRepository()
        {
            _sqlConnection =new SqlConnection("data source=(localdb)\\mssqllocaldb; database=Examples;");
        }
        public EmployeeData GetEmployeeById(int id)
        {
            //Take data from Table By Id
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "SELECT * FROM Employee WHERE ID = @id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", id);
                var sqlDataReader = sqlCommand.ExecuteReader();

                var employee = new EmployeeData();

                while (sqlDataReader.Read())
                {
                    employee.Id = (int)sqlDataReader["ID"];
                    employee.Name = (string)sqlDataReader["NAME"];
                    employee.Department = (string)sqlDataReader["DEPARTMENT"];
                    employee.Age = (int)sqlDataReader["AGE"];
                    employee.Address = (string)sqlDataReader["ADDRESS"];
                }

                return employee;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public IEnumerable<EmployeeData> GetEmployees()
        {
            //Take data from Table
            try
            {
                _sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: "SELECT * FROM Employee", _sqlConnection);

                var sqlDataReader = sqlCommand.ExecuteReader();

                var listOfEmployees = new List<EmployeeData>();

                while (sqlDataReader.Read())
                {
                    listOfEmployees.Add(new EmployeeData()
                    {
                        Id=(int)sqlDataReader["ID"],
                        Name=(string)sqlDataReader["NAME"],
                        Department=(string)sqlDataReader["DEPARTMENT"],
                        Age=(int)sqlDataReader["AGE"],
                        Address=(string)sqlDataReader["ADDRESS"]

                    });
                }
                return listOfEmployees;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }

        }
        //Create Methods For Table insert, update and Delete Here
        public bool InsertEmployee(EmployeeData employeeData)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "INSERT INTO Employee (NAME,DEPARTMENT,AGE,ADDRESS) " +
                    "VALUES(@name,@department,@age,@address)", _sqlConnection);

                sqlCommand.Parameters.AddWithValue("name", employeeData.Name);
                sqlCommand.Parameters.AddWithValue("department", employeeData.Department);
                sqlCommand.Parameters.AddWithValue("age", employeeData.Age);
                sqlCommand.Parameters.AddWithValue("address", employeeData.Address);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public bool UpdateEmployee(EmployeeData employeeData)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "UPDATE Employee SET NAME=@name,DEPARTMENT=@department,AGE=@age,ADDRESS=@address" +
                    " WHERE ID=@id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", employeeData.Id);
                sqlCommand.Parameters.AddWithValue("name", employeeData.Name);
                sqlCommand.Parameters.AddWithValue("department", employeeData.Department);
                sqlCommand.Parameters.AddWithValue("age", employeeData.Age);
                sqlCommand.Parameters.AddWithValue("address", employeeData.Address);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand(cmdText: "DELETE FROM Employee WHERE ID=@id", _sqlConnection);

                sqlCommand.Parameters.AddWithValue("id", id);

                var response= sqlCommand.ExecuteNonQuery();
                if (response > 0)
                {
                    return true;
                }
                return false;
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
