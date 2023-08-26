namespace ForumSystem.Services.Data.Interfaces
{
    using System.Collections.Generic;

    using ForumSystem.Common;

    public interface ICategoriesService : IService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);
    }
}
