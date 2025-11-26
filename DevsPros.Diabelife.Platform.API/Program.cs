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
using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Appointment.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Appointment.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Application.Internal.OutboundServices;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Glucometer.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Community.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Community.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Community.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Community.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Authentication.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Shared.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.CommandServices;
using DevsPros.Diabelife.Platform.API.Reports.Application.Internal.QueryServices;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Repositories;
using DevsPros.Diabelife.Platform.API.Reports.Infrastructure.Persistence.EFC.Repositories;

using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DevsPros.Diabelife.Platform.API.Community.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// PORT for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Load config
builder.Configuration.AddEnvironmentVariables();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DiabeLife API",
        Version = "v1",
        Description = "API for DiabeLife Platform",
        Contact = new OpenApiContact
        {
            Name = "DevsPros Team",
            Email = "devspros@diabelife.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insert JWT token",
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
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ==== CORS FIX ====
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowProductionFrontend", p => p
        .WithOrigins(
            "https://diabelife-frontend.netlify.app",
            "http://localhost:5173"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    );

    options.AddPolicy("AllowAllDev", p =>
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// ==== DB CONNECTION ====
var connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Database=diabelife;Uid=root;Pwd=password;";

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString);
});

// ==== JWT ====
var jwtKey = builder.Configuration["Jwt:Key"] ?? "super-secret-key-VERY-LONG-1234567890";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "DiabeLifeAPI";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "DiabeLifeClient";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
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

// ==== Repositories ====
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IHealthMetricRepository, HealthMetricRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IFoodDataRepository, FoodDataRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IGlucoseMeasurementRepository, GlucoseMeasurementRepository>();

builder.Services.AddScoped<ICommunityPostRepository, CommunityPostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// ==== Command Services ====
builder.Services.AddScoped<IHealthMetricCommandService, HealthMetricCommandService>();
builder.Services.AddScoped<IRecommendationCommandService, RecommendationCommandService>();
builder.Services.AddScoped<IFoodDataCommandService, FoodDataCommandService>();

builder.Services.AddScoped<IAuthCommandService, AuthCommandService>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();

builder.Services.AddScoped<NotificationCommandService>();
builder.Services.AddScoped<IAppointmentCommandService, AppointmentCommandService>();
builder.Services.AddScoped<IGlucoseMeasurementCommandService, GlucoseMeasurementCommandService>();

builder.Services.AddScoped<ICommunityCommandService, CommunityCommandService>();

// ==== Query Services ====
builder.Services.AddScoped<IHealthMetricQueryService, HealthMetricQueryService>();
builder.Services.AddScoped<IRecommendationQueryService, RecommendationQueryService>();
builder.Services.AddScoped<IFoodDataQueryService, FoodDataQueryService>();

builder.Services.AddScoped<IAuthQueryService, AuthQueryService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();

builder.Services.AddScoped<NotificationQueryService>();
builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();
builder.Services.AddScoped<IGlucoseMeasurementQueryService, GlucoseMeasurementQueryService>();

builder.Services.AddScoped<ICommunityQueryService, CommunityQueryService>();

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

var app = builder.Build();

// Swagger always ON
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiabeLife API v1");
    c.RoutePrefix = "swagger";
});

// CORS
if (app.Environment.IsDevelopment())
    app.UseCors("AllowAllDev");
else
    app.UseCors("AllowProductionFrontend");

// Auth
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Ensure DB creation
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
