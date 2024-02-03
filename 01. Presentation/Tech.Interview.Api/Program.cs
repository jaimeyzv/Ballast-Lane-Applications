using AutoMapper;
using MediatR;
using System.Reflection;
using Tech.Interview.Application.Features.Users.Commands;
using Tech.Interview.Application.Features.Users.Queries;
using Tech.Interview.Application.Persistence.UoW;
using Tech.Interview.Infrastructure.Mapper;
using Tech.Interview.Persistence.UoW;

var builder = WebApplication.CreateBuilder(args);

#region Service Configurations

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersModelResult>>, GetAllUsersHandler>();
builder.Services.AddScoped<IRequestHandler<GetUserByIdQuery, GetUserByIdModelResult>, GetUserByIdHandler>();
builder.Services.AddScoped<IRequestHandler<CreateUserCommand, bool>, CreateUserHandler>();
builder.Services.AddScoped<IRequestHandler<UpdateUserCommand, bool>, UpdateUserHandler>();
builder.Services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserHandler>();

// AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
