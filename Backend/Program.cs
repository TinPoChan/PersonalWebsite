using System.Text.Json.Serialization;
using Backend.Apis;
using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("ProjectDb");
builder.Services.AddSqlite<ProjectDbContext>(connString);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TagService>();

var app = builder.Build();

app.MapTagsEndpoints();

app.MapProjectsEndpoints();

await app.MigrateDBAsync();

app.Run();
