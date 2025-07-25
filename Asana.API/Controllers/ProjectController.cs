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
        public async Task<IEnumerable<Project>> Get()
        {
            return await new ProjectEC().GetProjects();
        }

        [HttpGet("Expand")]
        public async Task<IEnumerable<Project>> GetExpand()
        {
            return await new ProjectEC().GetProjects(true);
        }

        [HttpGet("Expand/{id}")]
        public async Task<Project?> GetExpandById(int id)
        {
            return await new ProjectEC().GetProjectById(id, true);
        }


        [HttpGet("{id}")]
        public async Task<Project?> GetById(int id)
        {
            return await new ProjectEC().GetProjectById(id);
        }

        [HttpDelete("{id}")]
        public Task<Project?> Delete(int id)
        {
            return new ProjectEC().Delete(id);
        }

        [HttpPost]
        public async Task<Project?> AddUpdate([FromBody] Project? Project)
        {
            return await new ProjectEC().AddUpdateProject(Project);
        }


    }
}
