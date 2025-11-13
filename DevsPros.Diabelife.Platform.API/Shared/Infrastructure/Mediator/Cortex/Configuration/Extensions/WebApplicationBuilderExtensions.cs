using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Mediator.Cortex.Configuration;

namespace ACME.LearningCenterPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddCortexConfigurationServices(this WebApplicationBuilder builder)
    {
        // Add Mediator Injection Configuration
        builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

        // Add Cortex Mediator for Event Handling
        builder.Services.AddCortexMediator(
            builder.Configuration,
            [typeof(Program)], options =>
            {
                options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
                //options.AddDefaultBehaviors();
            });
    }
}