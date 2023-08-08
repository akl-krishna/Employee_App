using Microsoft.AspNetCore.Mvc;
using Employee_Application.Model;
using Microsoft.Extensions.Hosting;
using Core_WebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employee_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        EmployeeDB dbobj = new EmployeeDB();
        // GET: api/<EmployeeController>
        [HttpGet]
        public List<Employee> Get()
        {
            return dbobj.SelectDB();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            var getemployee = dbobj.SelectDB().Where(x => x.id == id).FirstOrDefault();
            return getemployee;
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] Employee clsobj)
        {
            dbobj.InsertDB(clsobj);
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee clsobj)
        {
            var updateemp = dbobj.SelectDB().Where(x => x.id == id).FirstOrDefault();
            if (updateemp != null)
            {
                updateemp.esal = clsobj.esal;
                dbobj.updateDB(updateemp);
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            dbobj.DeleteDB(id);
        }
    }
}
