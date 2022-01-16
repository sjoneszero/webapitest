using CoraWebAPITest.DTO;
using CoraWebAPITest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoraWebAPITest.Controllers
{
    [ApiController]
    [Route("project")]
    public class ProjectController : ControllerBase
    {
        private readonly IDictionary<int, Project> Projects;
        private readonly ITimezoneLookup TimezoneLookup;
        private readonly IProjectValidator ProjectValidator;


        public ProjectController(
            IDictionary<int, Project> projects,
            ITimezoneLookup timezoneLookup,
            IProjectValidator projectValidator
            )
        {
            Projects = projects;
            TimezoneLookup = timezoneLookup;
            ProjectValidator = projectValidator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Projects.Values);
        }


        [HttpPost]
        public IActionResult Create([FromBody] Project item)
        {
            if (ProjectValidator.IsValid(Projects, item)
                && TimezoneLookup.GetTimezone(item.Timezone).isValid)
            {
                Projects.Add(Projects.Keys.Count + 1, item);
                return Ok(Projects);
            }
            else return BadRequest(); 
        }
    }
}
