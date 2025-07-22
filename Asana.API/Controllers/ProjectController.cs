using Asana.API.Database;
using Asana.API.Enterprise;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asana.Core.Models;

namespace Asana.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return new ProjectEC().GetProjects();
        }

        [HttpGet("Expand")]
        public IEnumerable<Project> GetExpand()
        {
            return new ProjectEC().GetProjects(true);
        }

        [HttpGet("Expand/{id}")]
        public Project? GetExpandById(int id)
        {
            return new ProjectEC().GetProjectById(id, true);
        }


        [HttpGet("{id}")]
        public Project? GetById(int id)
        {
            return new ProjectEC().GetProjectById(id);
        }

        [HttpDelete("{id}")]
        public Project? Delete(int id)
        {
            return new ProjectEC().Delete(id);
        }

        [HttpPost]
        public Project? AddUpdate([FromBody] Project? Project)
        {
            return new ProjectEC().AddUpdateProject(Project);
        }


    }
}
