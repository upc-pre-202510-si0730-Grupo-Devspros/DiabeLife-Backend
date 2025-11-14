using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.HealthyLife.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Notifications.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Notifications.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Notifications.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Notifications.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Appointment.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Glucometer.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;

using DevsPros.Diabelife.Platform.API.Community.Infrastructure.Interfaces.ASP.Configuration.Extensions;

using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Reports.Infrastructure.Persistence.EFC.Repositories;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration.Extensions;
using ACME.LearningCenterPlatform.API.Shared.Infrastructure.Mediator.Cortex.Configuration.Extensions;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DiabeLife API",
        Version = "v1",
        Description = "API for managing diabetes health metrics, food data, recommendations, authentication, community, and reports",
        Contact = new OpenApiContact { Name = "DevsPros Team", Email = "devspros@diabelife.com" }
    });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter your token below (without 'Bearer').",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlifyFrontend", policy =>
    {
        policy.WithOrigins("https://diabelife-frontend.netlify.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
    options.AddPolicy("AllowDevelopment", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost;Database=diabelife;Uid=root;Pwd=password;";
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

var jwtKey = builder.Configuration["Jwt:Key"] ?? "your-secret-key-here-make-it-longer-than-32-characters-for-security";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "DiabeLifeAPI";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "DiabeLifeClient";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IHealthMetricRepository, HealthMetricRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IFoodDataRepository, FoodDataRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IGlucoseMeasurementRepository, GlucoseMeasurementRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<IHealthMetricCommandService, HealthMetricCommandService>();
builder.Services.AddScoped<IRecommendationCommandService, RecommendationCommandService>();
builder.Services.AddScoped<IFoodDataCommandService, FoodDataCommandService>();
builder.Services.AddScoped<NotificationCommandService>();
builder.Services.AddScoped<IAppointmentCommandService, AppointmentCommandService>();
builder.Services.AddScoped<IGlucoseMeasurementCommandService, GlucoseMeasurementCommandService>();
builder.Services.AddScoped<IAuthCommandService, AuthCommandService>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();

builder.Services.AddScoped<IHealthMetricQueryService, HealthMetricQueryService>();
builder.Services.AddScoped<IRecommendationQueryService, RecommendationQueryService>();
builder.Services.AddScoped<IFoodDataQueryService, FoodDataQueryService>();
builder.Services.AddScoped<NotificationQueryService>();
builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();
builder.Services.AddScoped<IGlucoseMeasurementQueryService, GlucoseMeasurementQueryService>();
builder.Services.AddScoped<IAuthQueryService, AuthQueryService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();

builder.AddCommunityContextServices();
builder.AddSharedContextServices();
builder.AddCortexConfigurationServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiabeLife API v1");
    c.RoutePrefix = "swagger";
});

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowDevelopment");
}
else
{
    app.UseCors("AllowNetlifyFrontend");
    app.UseHttpsRedirection();
}
app.UseCors("AllowDevelopment");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
