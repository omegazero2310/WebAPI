using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using WebAPI.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{

    /// <summary>
    /// API controller for CRUD with Employee Data
    ///   <br />
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 8/9/2022 created
    /// annv3 8/9/2022 add DI and database
    /// </Modified>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : ControllerBase
    {
        
        public IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<POCOClass.Employee> Get()
        {
            using(var dbContext = new WebApiDBContext(_configuration))
            {
                return dbContext.Employees.ToList();
            }    
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public POCOClass.Employee Get(string id)
        {
            using (var dbContext = new WebApiDBContext(_configuration))
            {
                return dbContext.Employees.Where(emp => emp.Id == id).FirstOrDefault();
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] POCOClass.Employee value)
        {
            using (var dbContext = new WebApiDBContext(_configuration))
            {
                if (!dbContext.Employees.Any(emp => emp.Id == value.Id))
                {
                    dbContext.Employees.Add(value);
                    dbContext.SaveChanges();
                }
                    
            }
        }

        // PUT api/<EmployeeController>/
        [HttpPut()]
        public void Put([FromBody] POCOClass.Employee value)
        {
            using (var dbContext = new WebApiDBContext(_configuration))
            {
                dbContext.Update<POCOClass.Employee>(value);
                dbContext.SaveChanges();
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            using (var dbContext = new WebApiDBContext(_configuration))
            {
                POCOClass.Employee employee = new POCOClass.Employee() { Id = id };
                dbContext.Remove<POCOClass.Employee>(employee);
                dbContext.SaveChanges();
            }
        }
    }
}
