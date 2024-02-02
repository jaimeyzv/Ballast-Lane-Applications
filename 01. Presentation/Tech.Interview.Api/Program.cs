using AutoMapper;
using Tech.Interview.Application.Persistence;
using Tech.Interview.Application.Persistence.UoW;
using Tech.Interview.Infrastructure.Mapper;
using Tech.Interview.Persistence.Repositories;
using Tech.Interview.Persistence.UoW;

var builder = WebApplication.CreateBuilder(args);

#region Service Configurations

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

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
