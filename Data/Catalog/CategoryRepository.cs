using Data.Context;
using Data.Entities;
using Microsoft.Extensions.Logging;

namespace Data.Repisitories.Catalogs;

public class CategoryRepository(ApplicationDbContext context, ILogger<ICategoryRepository> logger)
    : BaseRepository<Category, ICategoryRepository>(context, logger), ICategoryRepository
{
    private readonly ILogger<ICategoryRepository> _logger1 = logger;
}