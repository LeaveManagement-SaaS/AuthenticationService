
using AuthenticationService.Domain.Interfaces;
using AuthenticationService.CrossCutting.Users.Commands.CreateUser;
using AuthenticationService.Infrastructure.DependencyInjection;
using AuthenticationService.Infrastructure.Persistence;
using AuthenticationService.Infrastructure.Repositories;
using MediatR;
using AuthenticationService.API.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Dependency Injection
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure HTTP Request Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalException();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();