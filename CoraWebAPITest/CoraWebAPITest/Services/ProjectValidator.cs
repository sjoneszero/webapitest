using System.Collections.Generic;
using System.Linq;

namespace CoraWebAPITest.Services
{
    public class ProjectValidator : IProjectValidator
    {
        private const int DbColumnConstraint = 50;

        public ProjectValidator()
        {
        }

        public bool IsValid(IDictionary<int, DTO.Project> projects, DTO.Project item)
        {
            if (item != null)
            {
                if (item.Name.Length <= DbColumnConstraint)
                {
                    if (!item.Name.StartsWith("Test"))
                    {
                        if (!item.Name.EndsWith("Test"))
                        {
                            if (!projects.Values.Any(project => project == item))
                            {
                                if (item.StartDate != null)
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

    }
}
