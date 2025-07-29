using movie_backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//register the TMDB service
builder.Services.AddHttpClient<TMDBService>();

//register swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:3000","http://localhost:5173") // React dev server
            .AllowAnyHeader()
            .AllowAnyMethod());
});

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
