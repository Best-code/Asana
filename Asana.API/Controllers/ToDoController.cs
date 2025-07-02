using Asana.API.Database;
using Asana.API.Enterprise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asana.Core.Models;

namespace Asana.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return new ToDoEC().GetToDos();
        }
    }
}
