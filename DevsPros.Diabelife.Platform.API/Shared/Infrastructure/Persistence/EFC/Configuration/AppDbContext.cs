using DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using DevsPros.Diabelife.Platform.API.HealthyLife.Domain.Model;

using DevsPros.Diabelife.Platform.API.Shared.Domain.Model;
using DevsPros.Diabelife.Platform.API.Reports.Domain.Model;
using DevsPros.Diabelife.Platform.API.Notifications.Domain.Model;
using DevsPros.Diabelife.Platform.API.Appointment.Domain.Model;
using DevsPros.Diabelife.Platform.API.Glucometer.Domain.Model;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.Entities;
using DevsPros.Diabelife.Platform.API.Community.Domain.Model.ValueObjects;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using CommunityPost = DevsPros.Diabelife.Platform.API.Community.Domain.Model.Aggregates.CommunityPost;

namespace DevsPros.Diabelife.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
/// Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // HealthyLife DbSets
    public DbSet<HealthMetric> HealthMetrics { get; set; }
    public DbSet<Recommendation> Recommendations { get; set; }
    public DbSet<FoodData> FoodData { get; set; }

    
    // Authentication & Reports DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Report> Reports { get; set; }

    // Notifications DbSets
    public DbSet<Notification> Notifications { get; set; }

    // Appointment DbSets
    public DbSet<AppointmentEntity> Appointments { get; set; }

    // Glucometer DbSets
    public DbSet<GlucoseMeasurement> GlucoseMeasurements { get; set; }

    // Community DbSets
    public DbSet<CommunityPost> CommunityPosts => Set<CommunityPost>();
    public DbSet<Comment> Comments => Set<Comment>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add created/updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //
        // ===== HEALTHY LIFE =====
        //
        builder.Entity<HealthMetric>(entity =>
        {
            entity.ToTable("health_metrics");
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(h => h.HeartRate).HasColumnName("heart_rate").IsRequired();
            entity.Property(h => h.Glucose).HasColumnName("glucose").IsRequired();
            entity.Property(h => h.Weight).HasColumnName("weight").IsRequired();
            entity.Property(h => h.BloodPressure).HasColumnName("blood_pressure").HasMaxLength(20).IsRequired();
            entity.Property(h => h.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(h => h.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        builder.Entity<Recommendation>(entity =>
        {
            entity.ToTable("recommendations");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(r => r.Text).HasColumnName("text").HasMaxLength(500).IsRequired();
            entity.Property(r => r.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(r => r.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        builder.Entity<FoodData>(entity =>
        {
            entity.ToTable("food_data");
            entity.HasKey(f => f.Id);
            entity.Property(f => f.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(f => f.Food).HasColumnName("food").HasMaxLength(200).IsRequired();
            entity.Property(f => f.Timestamp).HasColumnName("timestamp").IsRequired();
            entity.Property(f => f.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(f => f.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });


        // User Entity Configuration
        builder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(u => u.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
            entity.Property(u => u.PasswordHash).HasColumnName("password_hash").HasMaxLength(500).IsRequired();
            entity.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(u => u.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            // Unique constraint for email
            entity.HasIndex(u => u.Email).IsUnique();
        });

        // Report Entity Configuration
        builder.Entity<Report>(entity =>
        {
            entity.ToTable("reports");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(r => r.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(r => r.Date).HasColumnName("date").IsRequired();
            entity.Property(r => r.Type).HasColumnName("type").HasMaxLength(100).IsRequired();
            entity.Property(r => r.Data).HasColumnName("data").IsRequired();
            entity.Property(r => r.Selected).HasColumnName("selected").IsRequired();
            entity.Property(r => r.Shared).HasColumnName("shared").IsRequired();
            entity.Property(r => r.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(r => r.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(r => r.UpdatedAt).HasColumnName("updated_at").IsRequired();
            
            // Foreign key relationship
            entity.HasOne(r => r.User)
                  .WithMany(u => u.Reports)
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        //
        // ===== NOTIFICATIONS =====
        //
        builder.Entity<Notification>(entity =>
        {
            entity.ToTable("notifications");
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(n => n.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
            entity.Property(n => n.Message).HasColumnName("message").HasMaxLength(500).IsRequired();
            entity.Property(n => n.Type).HasColumnName("type").HasMaxLength(50).IsRequired();
            entity.Property(n => n.UserId).HasColumnName("user_id").HasMaxLength(100).IsRequired();
            entity.Property(n => n.IsRead).HasColumnName("is_read").IsRequired();
            entity.Property(n => n.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(n => n.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        //
        // ===== APPOINTMENTS =====
        //
        builder.Entity<AppointmentEntity>(entity =>
        {
            entity.ToTable("appointments");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(a => a.AppointmentDate).HasColumnName("appointment_date").IsRequired();
            entity.Property(a => a.Doctor).HasColumnName("doctor").HasMaxLength(200).IsRequired();
            entity.Property(a => a.Patient).HasColumnName("patient").HasMaxLength(200).IsRequired();
            entity.Property(a => a.AppointmentType).HasColumnName("appointment_type").HasMaxLength(100).IsRequired();
            entity.Property(a => a.Status).HasColumnName("status").HasMaxLength(50).IsRequired().HasDefaultValue("Scheduled");
            entity.Property(a => a.Notes).HasColumnName("notes").HasMaxLength(1000);
            entity.Property(a => a.Location).HasColumnName("location").HasMaxLength(300).IsRequired();
            entity.Property(a => a.Duration).HasColumnName("duration").IsRequired();
            entity.Property(a => a.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(a => a.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        //
        // ===== GLUCOMETER =====
        //
        builder.Entity<GlucoseMeasurement>(entity =>
        {
            entity.ToTable("glucose_measurements");
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(g => g.Value).HasColumnName("value").IsRequired();
            entity.Property(g => g.Unit).HasColumnName("unit").HasMaxLength(50).IsRequired();
            entity.Property(g => g.Status).HasColumnName("status").HasMaxLength(50);
            entity.Property(g => g.Trend).HasColumnName("trend").HasMaxLength(100);
            entity.Property(g => g.MeasurementDate).HasColumnName("measurement_date").IsRequired();
            entity.Property(g => g.CreatedAt).HasColumnName("created_at").IsRequired();
            entity.Property(g => g.UpdatedAt).HasColumnName("updated_at").IsRequired();
        });

        //
        // ===== COMMUNITY =====
        //
        // CommunityPost <-> Comments (1:N)
        builder.Entity<CommunityPost>()
            .HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey(c => c.PostId);

        // ValueObject conversions for CommunityPost
        builder.Entity<CommunityPost>()
            .Property(p => p.Id)
            .HasConversion(v => v.Value, v => new CommunityPostId(v))
            .ValueGeneratedNever();

        builder.Entity<CommunityPost>()
            .Property(p => p.AuthorId)
            .HasConversion(v => v.Value, v => new AuthorId(v));

        builder.Entity<CommunityPost>()
            .Property(p => p.Content)
            .HasConversion(v => v.Value, v => new Content(v));

        builder.Entity<CommunityPost>()
            .Property(p => p.ImageUrl)
            .HasConversion(v => v == null ? null : v.Value, v => v == null ? null : new ImageUrl(v));

        builder.Entity<Comment>()
            .Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Entity<Comment>()
            .Property(c => c.AuthorId)
            .HasConversion(v => v.Value, v => new AuthorId(v));

        builder.Entity<Comment>()
            .Property(c => c.AuthorName)
            .HasConversion(v => v.Value, v => new AuthorName(v));

        builder.Entity<Comment>()
            .Property(c => c.Content)
            .HasConversion(v => v.Value, v => new Content(v));

        builder.Entity<Comment>()
            .Property(c => c.PostId)
            .HasConversion(v => v.Value, v => new CommunityPostId(v));


        //
        // ===== GLOBAL NAMING CONVENTION =====
        //
        builder.UseSnakeCaseNamingConvention();
    }
}
