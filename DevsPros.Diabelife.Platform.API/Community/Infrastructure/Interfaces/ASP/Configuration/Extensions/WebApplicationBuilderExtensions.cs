using DevsPros.Diabelife.Platform.API.Community.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Community.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Community.Domain.Services;
using DevsPros.Diabelife.Platform.API.Community.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DevsPros.Diabelife.Platform.API.Community.Infrastructure.Interfaces.ASP.Configuration.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddCommunityContextServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICommunityPostRepository, CommunityPostRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();

        builder.Services.AddScoped<ICommunityCommandService, CommunityCommandService>();

        builder.Services.AddScoped<ICommunityQueryService, CommunityQueryService>();
    }
}