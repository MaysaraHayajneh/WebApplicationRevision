using Microsoft.AspNetCore.Mvc;
using WebApplicationRevision;
using WebApplicationRevision.Contratct;
using WebApplicationRevision.Filters;
using WebApplicationRevision.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers((MvcOptions opt) =>
{
	opt.Filters.Add<LogActivityFilter>(); // THIS IS GLOBAL FILTER
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

// RETRIVE TEH REQUIRED SERVICE IMPLEMNETATION FOR TEH SAME SERVICE TYPE  BY THE KEY
//builder.Services.AddKeyedScoped<IWeatherforcastService, WeatherForecastService>("key1");
//builder.Services.AddKeyedScoped<IWeatherforcastService, TesTService>("key2");

builder.Services.AssemblyRegistartionMethdo();


// USING FACTORY SO YOU CAN CONTROLL HOW TEH TYPE OF SERVICE WILL BE INSTANITAITE HOW TEH IMPLEMENTATION WILL BE 
// YOIU CONTROLL HOW TEH SERVICE WILL GET CREATED 

//builder.Services.AddScoped<IWeatherforcastService, TesTService>((IServiceProvider sp) =>
//{
//	// CONDITION LOGIC 
//	return new TesTService();
//});

// EACH TYPE WILL GET INSTANCE 

//builder.Services.AddSingleton<ITestService, WeatherForecastService>();
//builder.Services.AddSingleton<IWeatherforcastService, WeatherForecastService>();

//IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
//var logger = serviceProvider.GetService<ILogger<WeatherForecastService>>();

// OTH WILL GET THE SAME INSTANCE
//var ws = new WeatherForecastService(logger);
//builder.Services.AddSingleton<ITestService>(ws);
//builder.Services.AddSingleton<IWeatherforcastService>(ws);


var app = builder.Build(); // on buildethe DI perform service validation 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
