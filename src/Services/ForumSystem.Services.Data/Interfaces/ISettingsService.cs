namespace ForumSystem.Services.Data.Interfaces
{
    using System.Collections.Generic;

    using ForumSystem.Common;

    public interface ISettingsService : IService
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}
