using Asana.API.Database;
using Asana.API.Enterprise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asana.Core.Models;

namespace Asana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return new ToDoEC().GetToDos();
        }

        [HttpGet("{id}")]
        public ToDo? GetById(int id)
        {
            return new ToDoEC().GetToDoById(id);
        }

        [HttpDelete("{id}")]
        public ToDo? Delete(int id)
        {
            return new ToDoEC().Delete(id);
        }

        [HttpPost]
        public ToDo? AddUpdate([FromBody] ToDo? toDo)
        {
            return new ToDoEC().AddUpdateToDo(toDo);
        }


    }
}
