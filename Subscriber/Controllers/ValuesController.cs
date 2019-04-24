using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Subscriber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new [] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ActionResult<dynamic> Post([FromBody] string value)
        {
            return new { value };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<dynamic> Put(int id, [FromBody] string value)
        {
            return new { id, value };
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<dynamic> Delete(int id)
        {
            return new { id };
        }
    }
}
