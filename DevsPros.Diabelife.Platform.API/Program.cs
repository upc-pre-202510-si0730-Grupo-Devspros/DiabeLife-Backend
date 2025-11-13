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
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger/OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "DiabeLife API", 
        Version = "v1",
        Description = "API for managing diabetes health metrics, food data, and recommendations",
        Contact = new OpenApiContact
        {
            Name = "DevsPros Team",
            Email = "devspros@diabelife.com"
        }
    });
    c.EnableAnnotations();
});

// Configure CORS
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

// Configure MySQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=localhost;Database=diabelife;Uid=root;Pwd=password;";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString);
});

// Register Repositories
builder.Services.AddScoped<IHealthMetricRepository, HealthMetricRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IFoodDataRepository, FoodDataRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IGlucoseMeasurementRepository, GlucoseMeasurementRepository>();

// Register Command Services
builder.Services.AddScoped<IHealthMetricCommandService, HealthMetricCommandService>();
builder.Services.AddScoped<IRecommendationCommandService, RecommendationCommandService>();
builder.Services.AddScoped<IFoodDataCommandService, FoodDataCommandService>();
builder.Services.AddScoped<NotificationCommandService>();
builder.Services.AddScoped<IAppointmentCommandService, AppointmentCommandService>();
builder.Services.AddScoped<IGlucoseMeasurementCommandService, GlucoseMeasurementCommandService>();

// Register Query Services
builder.Services.AddScoped<IHealthMetricQueryService, HealthMetricQueryService>();
builder.Services.AddScoped<IRecommendationQueryService, RecommendationQueryService>();
builder.Services.AddScoped<IFoodDataQueryService, FoodDataQueryService>();
builder.Services.AddScoped<NotificationQueryService>();
builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();
builder.Services.AddScoped<IGlucoseMeasurementQueryService, GlucoseMeasurementQueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiabeLife API v1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI the default page
    });
    app.UseCors("AllowDevelopment");
}
else
{
    app.UseCors("AllowNetlifyFrontend");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.Run();