using ContentNet.Api.Extensions;
using ContentNet.Application;
using ContentNet.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddExceptionHandling();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationApiVersioning();
builder.Services.AddApplicationSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseExceptionHandling();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseApplicationSwagger();

app.Run();
