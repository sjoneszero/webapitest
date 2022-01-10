using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoraWebAPITest.Controllers
{
    [ApiController]
    [Route("Projet")]
    public class ProjectController : ControllerBase
    {
        private IDictionary<int, DTO.Project> projects { get;  set; }
        private ITimezoneLookup TimezoneLookup { get; set; }


        public ProjectController(
            IDictionary<int, DTO.Project> projects,
            ITimezoneLookup timezoneLookup)
        {
            this.projects = projects;
            this.TimezoneLookup = timezoneLookup;
        }

        [HttpGet]
        public IEnumerable<DTO.Project> Get()
        {
            return this.projects.Values;
        }

        private bool IsValid(DTO.Project item)
        {
            var _dbColumnConstraint = 50;

            if (item != null)
            {
                if (item.Name.Length <= _dbColumnConstraint)
                {
                    if (!item.Name.StartsWith("Test"))
                    {
                        if (!item.Name.EndsWith("Test"))
                        {
                            if (!projects.Values.Any(project => project == item))
                            {
                                if(item.StartDate != null)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }


        private bool IsValidTimezone(DTO.Project item)
        {
            var timezoneLookupResult = TimezoneLookup.GetTimezone(item.Timezone);

            if (timezoneLookupResult.isValid != true)
            {
                return false;
            }

            return true;
        }


        [HttpGet("AddItemToArray", Name = "AddItemToArray")]
        public IDictionary<int, DTO.Project> AddItemToArray([FromQuery] DTO.Project item)
        {
            this.IsValid(item);
            this.IsValidTimezone(item);

            projects.Add(projects.Keys.Count + 1, item);

            return projects;
        }
    }
}
