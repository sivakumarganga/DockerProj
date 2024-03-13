using System.Text.Json.Serialization;

Console.WriteLine("Application Running");
if (args != null && args.Any())
{
    Console.WriteLine(string.Join(",", args));
}
var customEnv = Environment.GetEnvironmentVariable("CustomEnvFromDocker");
if (customEnv is not null)
{
    Console.WriteLine($"Found Env :{customEnv}");
}
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/weatherforecast", () =>
{

    string error1 = string.Empty, error2 = string.Empty;
    string url="http://apiservice:5020/ping";
    try
    {
        HttpClient client = new HttpClient();
        var stringResult = client.GetStringAsync(url).GetAwaiter().GetResult();
        return $"secondapi {url}:{stringResult}";
        //return System.Text.Json.JsonSerializer.Deserialize<WeatherForecast[]>(stringResult);
    }
    catch (Exception ex)
    {
        error1 = $"Failed to Retrieve from Secondapi {url} Message:{ex.Message}";
        Console.WriteLine(error1);
    }
     url="http://localhost:5020/ping";
    try
    {
        HttpClient client = new HttpClient();
        var stringResult = client.GetStringAsync(url).GetAwaiter().GetResult();
        return $"localhost:{stringResult}";
    }
    catch (Exception ex)
    {
        error2 = $"Failed to Retrieve from Secondapi {url} Message:{ex.Message}";
        Console.WriteLine(error2);
    }

    return $"Unable to contact those services Message1:{error1} Message2:{error2}";
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

