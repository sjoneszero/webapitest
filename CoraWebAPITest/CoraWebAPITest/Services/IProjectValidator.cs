using System.Collections.Generic;
using CoraWebAPITest.DTO;

namespace CoraWebAPITest.Services
{
    public interface IProjectValidator
    {
        bool IsValid(IDictionary<int, Project> projects, Project item);
    }
}