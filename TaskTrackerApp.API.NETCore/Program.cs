using TaskTrackerApp.API.NETCore.DAL;
using TaskTrackerApp.API.NETCore.Service;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(origin =>
        {
            if (string.IsNullOrWhiteSpace(origin)) return false;
            // Allow any localhost origin for development
            return origin.StartsWith("http://localhost") || origin.StartsWith("https://localhost");
        })
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

// Add support for serving static files (Angular)
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors();

// Add some logging for debugging
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path} from {context.Request.Headers.Origin}");
    await next();
});

app.UseAuthorization();

// Enable serving static files
app.UseStaticFiles();
app.UseSpaStaticFiles();

// Map API controllers
app.MapControllers();

// Configure SPA (Angular)
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot";

    if (app.Environment.IsDevelopment())
    {
        // In development, you could proxy to Angular dev server
        // spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    }
    else
    {
        // In production, serve the built Angular files
        spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
        {
            OnPrepareResponse = context =>
            {
                // Cache static assets for 1 day, but not the index.html
                if (!context.File.Name.Equals("index.html", StringComparison.OrdinalIgnoreCase))
                {
                    context.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400");
                }
            }
        };
    }
});

app.Run();

public partial class Program { }