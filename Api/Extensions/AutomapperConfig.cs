using TodoList.Mappings;

namespace TodoList.Extensions;

public static class AutoMapperConfig
{
    public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<CategoryProfile>();
            cfg.AddProfile<TaskProfile>();
            cfg.AddProfile<ProfileProfile>();
        }, typeof(UserProfile).Assembly);

        return services;
    }
}