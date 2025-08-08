using Data.Context;
using Data.Entities;
using Data.Repositories;

namespace Data.Repisitories.Catalogs;

public class CategoryRepository(ApplicationDbContext context)
    : BaseRepository<Category>(context), ICategoryRepository
{
    
}