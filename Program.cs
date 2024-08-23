using DemoWebAPI.Models;
using DemoWebAPI.Services;
using DemoWebAPI.Context;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Replace the placeholder in the configuration
builder.Configuration["Database:ConnectionURI"] = Environment.GetEnvironmentVariable("MONGO_URL");


// configuration for mongodb
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));
builder.Services.AddSingleton<MongoDbContext>();

// Adding the Services as Singleton
builder.Services.AddSingleton<StudentServices>();
builder.Services.AddSingleton<FacultyServices>();
builder.Services.AddSingleton<AdminServices>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding Controllers
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();
