using Data.Context;
using Microsoft.Extensions.Logging;

namespace Data.Repisitories.Catalogs;

public class TaskRepository(ApplicationDbContext context, ILogger<ITaskRepository> logger)
    : BaseRepository<Task, ITaskRepository>(context, logger), ITaskRepository
{

}