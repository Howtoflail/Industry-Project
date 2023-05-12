using System.Reflection;
using KindRegardsApi.Host;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc().AddApplicationPart(Assembly.Load(new AssemblyName("KindRegardsApi.Presentation")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostLayer(builder.Configuration);

builder.WebHost.UseKestrel();
//builder.WebHost.UseUrls("http://*:8080");
builder.WebHost.UseIISIntegration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.UsePathBase("/api/v1");
app.UseRouting();

app.Run();
