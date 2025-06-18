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
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Add CORS Policy for Frontend Integration

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendPolicy",
        policy => policy.WithOrigins("http://localhost:3000", "http://localhost:5173", "http://127.0.0.1:3000", "http://127.0.0.1:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
    
    // Keep AllowAll for development/testing purposes
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


if(connectionString== null) throw new InvalidOperationException("Connection string not found.");



builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

builder.Services.AddSwaggerGen(options=> { options.EnableAnnotations(); });

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// CRM Bounded Context
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomRespository, RoomRepository>();
builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
builder.Services.AddScoped<IHotelCommandService, HotelCommandService>();
builder.Services.AddScoped<IHotelQueryService, HotelQueryService>();
builder.Services.AddScoped<IRoomCommandService, RoomCommandService>();
builder.Services.AddScoped<IRoomQueryService, RoomQueryService>();
builder.Services.AddScoped<IServiceRequestCommandService, ServiceRequestCommandService>();
builder.Services.AddScoped<IServiceRequestQueryService, ServiceRequestQueryService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy - Use specific frontend policy in production
if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAllPolicy"); // More permissive for development
}
else
{
    app.UseCors("AllowFrontendPolicy"); // Restricted to frontend origins
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();