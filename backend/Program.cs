using backend.Interfaces;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register the WikipediaService with the IWikipediaService interface
builder.Services.AddScoped<IWikipediaService, WikipediaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "https://localhost:5173") //url of frontend
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// For Swagger testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

// Redirects to the stated Https url 
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
