using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SenwesAssignment_API.ApiErrors;
using SenwesAssignment_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SenwesAssignment_API.Controllers
{
    [Produces("application/json")]
    [ApiController, Authorize]  
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly LoadData _loadData;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
            _loadData = new LoadData();
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns>Returns a list of all employees</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var employeeData = _loadData.LoadEmployeeData();
            return Ok(employeeData);
        }

        
        [Route("Get/{empId}")]
        [HttpGet]
        public IActionResult GetByEmployeeId(int empId)
        {
            var employee = _loadData.LoadEmployeeData().Where(x => x.EmpID == empId).FirstOrDefault();

            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound(new NotFoundError("The user was not found"));
            }

        }

       
        [HttpGet("DateOfJoining")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeByDate()
        {
            var years = 365 * 5;

            var cutoff = DateTime.Now.Subtract(new TimeSpan(years, 0, 0, 0));


            //var employeeSalary = _loadData.LoadEmployeeData().Where(x => x.DateOfJoining.Contains(date) && ).ToList();  a=>a.DateCreated<cutoff
            var employee = _loadData.LoadEmployeeData().Where(x => Convert.ToDateTime(x.DateOfJoining) < cutoff).ToList(); 

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }


        [HttpGet("Age")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeByAge(float age = 30)
        {
            var employee = _loadData.LoadEmployeeData().Where(x => x.Age > age).ToList();

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }


        [HttpGet("TopPaid")]
        public async Task<ActionResult<List<Employee>>> GetEmployeeBySalary()
        {
            var employee = _loadData.LoadEmployeeData().OrderByDescending(e => e.Salary).Take(10).ToList();

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpGet("City")]
        public dynamic GetEmployeeByCityName(string name)
        {
            var employee = _loadData.LoadEmployeeData().Where(x => x.FirstName == name).Select(i => new { i.FirstName, i.City }).ToList();

            if (employee != null)
            {
                return employee;
            }
            else
            { 
               return NotFound();
            }

            
        }


       
        [HttpGet("SalaryEm")]
        public dynamic GetEmployeeNameSalary2(string firstName = "Treasure")
        {
            var employeeSalary = _loadData.LoadEmployeeData().Where(x => x.FirstName == firstName).Select(i => new { i.Salary }).ToList();

            if (employeeSalary != null)
            {

                return employeeSalary;
            }
            else
            {
                return NotFound();
            }


        }


        
        [HttpGet("CityName"), AllowAnonymous]
        public dynamic GetEmployeeByCityName()
        {
            var employee = _loadData.LoadEmployeeData().Select(i => new { i.City }).ToList();

            if (employee != null)
            {
                return employee;
            }
            else
            {
                return NotFound();
            }


        }







    }
}
