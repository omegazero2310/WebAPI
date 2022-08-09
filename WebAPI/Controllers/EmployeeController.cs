using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        /// <summary>The list employee</summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 8/9/2022 created
        /// </Modified>
        private static List<POCOClass.Employee> _ListEmployee = new List<POCOClass.Employee>() { new POCOClass.Employee() { Id = "1", Name = "An", DateOfBirth = DateTime.Now, ExtraInfo = "123" } };

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<POCOClass.Employee> Get()
        {
            return _ListEmployee;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public POCOClass.Employee Get(string id)
        {
            return _ListEmployee.Where(emp => emp.Id == id).FirstOrDefault();
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] POCOClass.Employee value)
        {
            if (!_ListEmployee.Any(emp => emp.Id == value.Id))
                _ListEmployee.Add(value);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] POCOClass.Employee value)
        {
            int index = _ListEmployee.FindIndex(emp => emp.Id == value.Id);
            if (index != -1)
                _ListEmployee[index] = value;
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            int index = _ListEmployee.FindIndex(emp => emp.Id == id);
            if (index != -1)
                _ListEmployee.RemoveAt(index);
        }
    }
}
