using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using VNG.SocialNotify.Events;
using VNG.SocialNotify.Services;

namespace VNG.SocialNotify;

public class DependencyInjection
{
    private static readonly IServiceCollection services = new ServiceCollection();
    public static IServiceProvider ServiceProvider => services.BuildServiceProvider();
    public static void Load()
    {
        AddEventPubSub();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
    }

    // This method registers the EventPublisher instance as a transient service,
    // and then discovers and registers all implementations of IEventSubcriber<> interfaces.
    // The registered services can be used to publish and subscribe to events in the application.
    private static void AddEventPubSub()
    {
        services.AddTransient<IEventPublisher>(s => new EventPublisher(s.GetRequiredService));
        var @implementations = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(
                x => x is { IsClass: true, IsAbstract: false }
                    && x.GetInterfaces().Any(
                        itf => itf.IsGenericType && itf.GetGenericTypeDefinition() == typeof(IEventSubcriber<>)
                    )
            )
            .Select(
                x =>
                {
                    var @interface = x.GetInterfaces().Single(itf => itf.IsGenericType && itf.GetGenericTypeDefinition() == typeof(IEventSubcriber<>));
                    return (service: @interface, implement: x);
                }
            );
        foreach (var (service, implement) in @implementations)
            services.AddTransient(service, implement);
    }
}
