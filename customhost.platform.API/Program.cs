using customhost_backend.GuestExperience.Application.Internal.CommandServices;
using customhost_backend.GuestExperience.Application.Internal.QueryServices;
using customhost_backend.GuestExperience.Domain.Repositories;
using customhost_backend.GuestExperience.Domain.Services;
using customhost_backend.GuestExperience.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.Shared.Infrastructure.Interfaces.ASP.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using customhost_backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.Shared.Domain.Repositories;
using customhost_backend.crm.Domain.Repositories;
using customhost_backend.crm.Domain.Services;
using customhost_backend.crm.Application.Internal.CommandServices;
using customhost_backend.crm.Application.Internal.QueryServices;
using customhost_backend.crm.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.crm.Infrastructure.Repositories;
using customhost_backend.crm.Infrastructure.Persistence.Repositories;
using customhost_backend.billings.Domain.Repositories;
using customhost_backend.billings.Domain.Services;
using customhost_backend.billings.Application.Internal.CommandServices;
using customhost_backend.billings.Application.Internal.QueryServices;
using customhost_backend.billings.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.profiles.Domain.Repositories;
using customhost_backend.profiles.Domain.Services;
using customhost_backend.profiles.Application.Internal.CommandServices;
using customhost_backend.profiles.Application.Internal.QueryServices;
using customhost_backend.profiles.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.analytics.Domain.Repositories;
using customhost_backend.analytics.Domain.Services;
using customhost_backend.analytics.Domain.Services.External;
using customhost_backend.analytics.Application.Internal.QueryServices;
using customhost_backend.analytics.Infrastructure.Persistence.EFC.Repositories;
using customhost_backend.analytics.Infrastructure.ACL.External;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuraciones críticas para producción
if (builder.Environment.IsProduction())
{
    // HSTS para HTTPS obligatorio
    builder.Services.AddHsts(options =>
    {
        options.Preload = true;
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromDays(365);
    });
    
    // Redirección HTTPS
    builder.Services.AddHttpsRedirection(options =>
    {
        options.HttpsPort = 443;
    });
}

// Health Checks para monitoreo
builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Add CORS Policy for Frontend Integration - CONFIGURACIÓN CRÍTICA DE SEGURIDAD

builder.Services.AddCors(options =>
{
    if (builder.Environment.IsProduction())
    {
        // PRODUCCIÓN: CORS RESTRICTIVO - CRÍTICO PARA SEGURIDAD
        var allowedOrigins = builder.Configuration["Cors:AllowedOrigins"]?.Split(',') 
                            ?? throw new InvalidOperationException("CORS AllowedOrigins not configured for production");
        
        options.AddPolicy("ProductionCorsPolicy",
            policy => policy.WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
    }
    else
    {
        // DESARROLLO: Más permisivo
        options.AddPolicy("AllowFrontendPolicy",
            policy => policy.WithOrigins("http://localhost:3000", "http://localhost:5173", "http://127.0.0.1:3000", "http://127.0.0.1:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        
        options.AddPolicy("AllowAllPolicy",
            policy => policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    }
});


// CONFIGURACIÓN CRÍTICA DE BASE DE DATOS
if(connectionString== null) throw new InvalidOperationException("Connection string not found.");

// Expandir variables de entorno en producción
if (builder.Environment.IsProduction())
{
    connectionString = Environment.ExpandEnvironmentVariables(connectionString);
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableServiceProviderCaching()
            .EnableSensitiveDataLogging(false); // CRÍTICO: Deshabilitar en producción
});

// CRÍTICO: Swagger SOLO en desarrollo
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(options=> { options.EnableAnnotations(); });
}

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// CRM Bounded Context
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IStaffMemberRepository, StaffMemberRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IHotelCommandService, HotelCommandService>();
builder.Services.AddScoped<IHotelQueryService, HotelQueryService>();
builder.Services.AddScoped<IBookingCommandService, BookingCommandService>();
builder.Services.AddScoped<IBookingQueryService, BookingQueryService>();
builder.Services.AddScoped<IStaffMemberCommandService, StaffMemberCommandService>();
builder.Services.AddScoped<IStaffMemberQueryService, StaffMemberQueryService>();
builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();
builder.Services.AddScoped<IServiceRequestCommandService, ServiceRequestCommandService>();
builder.Services.AddScoped<IServiceRequestQueryService, ServiceRequestQueryService>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();

// Billings Bounded Context
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentCommandService, PaymentCommandService>();
builder.Services.AddScoped<IPaymentQueryService, PaymentQueryService>();

// Profiles Bounded Context
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// Analytics Bounded Context
// Repositories
builder.Services.AddScoped<IAnalyticsSnapshotRepository, AnalyticsSnapshotRepository>();
builder.Services.AddScoped<IMetricDataRepository, MetricDataRepository>();

// Query Services
builder.Services.AddScoped<IAnalyticsSnapshotQueryService, AnalyticsSnapshotQueryService>();
builder.Services.AddScoped<IAnalyticsMetricQueryService, AnalyticsMetricQueryService>();

// ACL Facades
builder.Services.AddScoped<IGuestExperienceContextFacade, GuestExperienceContextFacade>();
builder.Services.AddScoped<ICrmContextFacade, CrmContextFacade>();
builder.Services.AddScoped<IBillingsContextFacade, BillingsContextFacade>();

// GuestExperience Bounded Context
// Repositories
builder.Services.AddScoped<IIoTDeviceRepository, IoTDeviceRepository>();
builder.Services.AddScoped<IRoomDeviceRepository, RoomDeviceRepository>();
builder.Services.AddScoped<IRoomDevicePreferenceRepository, RoomDevicePreferenceRepository>();
builder.Services.AddScoped<IUserDevicePreferenceRepository, UserDevicePreferenceRepository>();

// Command Services
builder.Services.AddScoped<IIoTDeviceCommandService, IoTDeviceCommandService>();
builder.Services.AddScoped<IRoomDeviceCommandService, RoomDeviceCommandService>();
builder.Services.AddScoped<IRoomDevicePreferenceCommandService, RoomDevicePreferenceCommandService>();
builder.Services.AddScoped<IUserDevicePreferenceCommandService, UserDevicePreferenceCommandService>();

// Query Services
builder.Services.AddScoped<IIoTDeviceQueryService, IoTDeviceQueryService>();
builder.Services.AddScoped<IRoomDeviceQueryService, RoomDeviceQueryService>();
builder.Services.AddScoped<IRoomDevicePreferenceQueryService, RoomDevicePreferenceQueryService>();
builder.Services.AddScoped<IUserDevicePreferenceQueryService, UserDevicePreferenceQueryService>();


var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// CONFIGURACIÓN CRÍTICA DEL PIPELINE DE PRODUCCIÓN
if (app.Environment.IsProduction())
{
    // HSTS y HTTPS obligatorios en producción
    app.UseHsts();
    app.UseHttpsRedirection();
      // Headers de seguridad críticos
    app.Use(async (context, next) =>
    {
        context.Response.Headers["X-Frame-Options"] = "DENY";
        context.Response.Headers["X-Content-Type-Options"] = "nosniff";
        context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
        context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
        await next();
    });
    
    // CORS restrictivo para producción
    app.UseCors("ProductionCorsPolicy");
}
else
{
    // CRÍTICO: Swagger SOLO en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // CORS permisivo para desarrollo
    app.UseCors("AllowAllPolicy");
}

// Health checks endpoint
app.MapHealthChecks("/health");

app.UseAuthorization();

app.MapControllers();

app.Run();