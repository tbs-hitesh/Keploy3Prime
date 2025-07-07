var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

// 1. Route parameter: /greet/{name}
app.MapGet("/greet/{name}", (string name) =>
{
    return $"Hello, {name}!";
});

// 2. Query parameter: /add?a=5&b=7
app.MapGet("/add", (int a, int b) =>
{
    return new { Sum = a + b };
});

// 3. Request body (POST): /reverse
app.MapPost("/reverse", (InputText input) =>
{
    var reversed = new string(input.Text.Reverse().ToArray());
    return new { Original = input.Text, Reversed = reversed };
});


//4. Query parameter: /subtract?a=10&b=3
app.MapGet("/subtract", (int a, int b) =>
{
    return new { Sum = a - b };
});


//5 Division: /divide?a=10&b=2
app.MapGet("/divide", (int a, int b) =>
{
    if (b == 0)
    {
        return Results.BadRequest("Division by zero is not allowed.");
    }
    return new { Quotient = (double)a / b };
});



app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// For POST /reverse
internal record InputText(string Text);
