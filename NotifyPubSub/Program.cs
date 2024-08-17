
DependencyInjection.Load();

var serviceProvider = DependencyInjection.ServiceProvider;

 IEventPublisher eventPublisher = serviceProvider.GetRequiredService<IEventPublisher>();

 eventPublisher.Publish(
    new NotifyToFollower(){
        Message = "Hello, World!",
        UserFollowerId = 1
    }
 );