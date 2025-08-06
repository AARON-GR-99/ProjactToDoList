namespace Data.Repisitories.Catalogs;

public interface ITaskRepository : IBaseRepository<Task>
{
    Task<IEnumerable<Task>> GetActiveTasksAsync();
}