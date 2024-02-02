using AutoMapper;
using Tech.Interview.Infrastructure.Mapper;

var builder = WebApplication.CreateBuilder(args);

#region Service Configurations

// Add services to the container.

builder.Services.AddControllers();

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

app.Run();
