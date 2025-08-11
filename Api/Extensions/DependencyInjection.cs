using Api.Services.Catalogs;
using Api.Services.Extencion;
using Data.Catalog;
using Data.Repisitories.Catalogs;

namespace TodoList.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Servicios
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IHashService, HashService>();

        // Repositorios
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();

        return services;
    }
}