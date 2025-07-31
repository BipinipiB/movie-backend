using Microsoft.EntityFrameworkCore;
using movie_backend.DataAccess;
using movie_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//register the HTTPclient..this tells us call TMDB
builder.Services.AddHttpClient<TMDBService>();

//register swagger services
builder.Services.AddEndpointsApiExplorer();
//swagger is helpful for documentation and testing
builder.Services.AddSwaggerGen();

//Enable CORS
//CORS allows frontend origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:5173") // React dev server "http://localhost:3000",
            .AllowAnyHeader()
            .AllowAnyMethod());
});

//register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//apply CORS
app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
