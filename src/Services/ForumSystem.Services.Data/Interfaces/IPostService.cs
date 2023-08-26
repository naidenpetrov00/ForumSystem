namespace ForumSystem.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumSystem.Common;

    public interface IPostService : IService
    {
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}
