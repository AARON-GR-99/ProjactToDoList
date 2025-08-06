using Data.Entities;

namespace Data.Repisitories.Catalogs;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<IEnumerable<Category>> GetActiverCategoriesAsync();
}