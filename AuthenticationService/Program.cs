using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AuthenticationService.API.Middleware;
using AuthenticationService.Application.Behaviors;
using AuthenticationService.CrossCutting.Mapping;
using AuthenticationService.CrossCutting.Users.Commands.CreateUser;
using AuthenticationService.Domain.Interfaces;
using AuthenticationService.Infrastructure.DependencyInjection;
using AuthenticationService.Infrastructure.Persistence;
using AuthenticationService.Infrastructure.Repositories;
using AuthenticationService.Infrastructure.Security;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;



// Creates a WebApplication builder.
// This is the starting point of every ASP.NET Core application.
var builder = WebApplication.CreateBuilder(args);

#region Register Application Services

// Registers MVC Controllers.
// Required for handling HTTP requests through API controllers.
builder.Services.AddControllers();

// -------------------------
// JWT Authentication
// -------------------------

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });
// -------------------------
// FluentValidation
// -------------------------

// Enables automatic model validation using FluentValidation.
// Validation runs before the request reaches the controller.
//builder.Services.AddFluentValidationAutoValidation();

// Scans the assembly and automatically registers all validators.
// Here it starts scanning from CreateUserCommandValidator.
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();


// -------------------------
// Swagger / OpenAPI
// -------------------------

// Generates API endpoint metadata.
builder.Services.AddEndpointsApiExplorer();

// Registers Swagger generator.
// Used for API documentation and testing.
builder.Services.AddSwaggerGen();


// -------------------------
// Infrastructure Layer
// -------------------------

// Registers all Infrastructure dependencies.
// Usually contains:
// - Repository registrations
// - Database configuration
// - External services
// - Logging
// - Cache
builder.Services.AddInfrastructure(builder.Configuration);


// -------------------------
// Entity Framework Core
// -------------------------

// Registers ApplicationDbContext with Dependency Injection.
// Configures SQL Server using the connection string from appsettings.json.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


// -------------------------
// MediatR (CQRS)
// -------------------------

// Registers all MediatR handlers.
// It scans the assembly containing CreateUserHandler and automatically
// registers all Command and Query handlers.
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>();


    cfg.AddOpenBehavior(
        typeof(ValidationBehavior<,>));


    cfg.AddOpenBehavior(
        typeof(LoggingBehavior<,>));

    cfg.AddOpenBehavior(typeof(ExceptionHandlingBehavior<,>));

    cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));

    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});


// -------------------------
// AutoMapper
// -------------------------

// Registers AutoMapper profiles.
// UserProfile contains object-to-object mapping configuration.
// Example:
// User -> UserDto
// CreateUserCommand -> User
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<UserProfile>();
});


// -------------------------
// Repository Registration
// -------------------------

// Registers IUserRepository with its concrete implementation.
// Scoped lifetime means one instance is created per HTTP request.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

#endregion


// Builds the application.
// After this point, services cannot be added.
var app = builder.Build();

#region Configure HTTP Request Pipeline

// Enable Swagger only in Development environment.
// Prevents exposing API documentation in Production.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// -------------------------
// Global Exception Middleware
// -------------------------

// Custom middleware that catches unhandled exceptions
// and returns a standardized error response.
app.UseGlobalException();


// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

app.UseAuthentication();
// Enables Authorization middleware.
// Required when using [Authorize] attributes.
app.UseAuthorization();


// Maps controller endpoints.
// Example:
// api/User
// api/Auth
app.MapControllers();

#endregion


// Starts the application and begins listening for HTTP requests.
app.Run();