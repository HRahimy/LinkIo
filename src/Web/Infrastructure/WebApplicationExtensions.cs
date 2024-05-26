using System.Reflection;

namespace LinkIo.Web.Infrastructure;

public static class WebApplicationExtensions
{
    public static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group, string? overrideName = null)
    {
        var groupName = group.GetType().Name;

        if (overrideName != null)
        {
            app.UsePathBase($"/{overrideName}");
        }

        return app
            .MapGroup(overrideName ?? $"/api/{groupName}")
            .WithGroupName(overrideName ?? groupName)
            .WithTags(overrideName ?? groupName)
            .WithOpenApi();
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                instance.Map(app);
            }
        }

        return app;
    }
}
