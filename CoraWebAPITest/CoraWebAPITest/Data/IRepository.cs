using System;


namespace CoraWebAPITest.Data
{
    interface IProjectRepository
    {
        string GetAllProjectItems();

        void SaveNewProjectProjectItem();

        void DeleteProjectItem();
    }
}
