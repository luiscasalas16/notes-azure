namespace NetContainerAppsWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //CORS
            builder.Services.AddCors();

            var app = builder.Build();

            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //CORS
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.MapGet("/", () => "net-static-web-app");

            var summaries = new[]
            {
                "Freezing",
                "Bracing",
                "Chilly",
                "Cool",
                "Mild",
                "Warm",
                "Balmy",
                "Hot",
                "Sweltering",
                "Scorching"
            };

            app.MapGet(
                    "/api/weatherforecast",
                    () =>
                    {
                        return Enumerable
                            .Range(1, 5)
                            .Select(index => new WeatherForecast(
                                Date: DateTime.Now.AddDays(index),
                                Temperature: Random.Shared.Next(-20, 55),
                                Summary: summaries[Random.Shared.Next(summaries.Length)]
                            ))
                            .ToArray();
                    }
                )
                .WithName("GetWeatherForecast")
                .WithOpenApi();

            app.Run();
        }

        record WeatherForecast(DateTime Date, int Temperature, string? Summary);
    }
}
