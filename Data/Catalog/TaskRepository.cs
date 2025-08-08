using Data.Context;
using Data.Repositories;

namespace Data.Repisitories.Catalogs;

public class TaskRepository(ApplicationDbContext context)
    : BaseRepository<Entities.Task>(context), ITaskRepository
{

}