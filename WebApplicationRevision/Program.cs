using Microsoft.AspNetCore.Mvc;
using WebApplicationRevision;
using WebApplicationRevision.Contratct;
using WebApplicationRevision.Filters.ActionFilters;
using WebApplicationRevision.Filters.AuthorizationFilter;
using WebApplicationRevision.Filters.ExceptionFilter;
using WebApplicationRevision.Filters.RessourceFilter;
using WebApplicationRevision.Filters.ResultFiltet;
using WebApplicationRevision.Filters.TestTypeFilter;
using WebApplicationRevision.Middlewares;
using WebApplicationRevision.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers((MvcOptions opt) =>
{
    //opt.Filters.Add<LogActivityFilter>(); // THIS IS GLOBAL FILTER
    opt.Filters.Add<LogActivityFilterAsync>(); // THIS IS GLOBAL FILTER
    opt.Filters.Add<CacheFilterFilter>(); // THIS IS GLOBAL FILTER
    opt.Filters.Add<CacheFilterFilter2>(); // THIS IS GLOBAL FILTER
                                           //opt.ValueProviderFactories.Add(new CustomValueProviderFactory()); // THIS IS GLOBAL VALUE PROVIDER FACTORY

    opt.Filters.Add<GlobalExceptionFilter>(); // THIS IS GLOBAL FILTER
    opt.Filters.Add<ResponseWrappingResultFilter>(); // THIS IS GLOBAL FILTER
    opt.Filters.Add<CustomAuthorize>(); // THIS IS GLOBAL FILTER

});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

// RETRIVE TEH REQUIRED SERVICE IMPLEMNETATION FOR TEH SAME SERVICE TYPE  BY THE KEY
//builder.Services.AddKeyedScoped<IWeatherforcastService, WeatherForecastService>("key1");
//builder.Services.AddKeyedScoped<IWeatherforcastService, TesTService>("key2");

builder.Services.AssemblyRegistartionMethdo();
builder.Services.AddScoped(typeof(CustomAuthorize));
builder.Services.AddSingleton<RequestLoggingMiddleWare>();
builder.Services.AddMemoryCache();


// factory pattern
builder.Services.AddScoped<IWeatherforcastService>((IServiceProvider sp) =>
{
    var congig = sp.GetRequiredService<IConfiguration>();
    var useStrip = congig.GetValue<bool>("UseStripService");
    return useStrip ? sp.GetRequiredService<TesTService>() : sp.GetRequiredService<TesTService>();
});

//builder.Services.Configure<ApiBehaviorOptions>((ApiBehaviorOptions opt) =>
//{
//	opt.SuppressModelStateInvalidFilter = true;
//});
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
app.UseResponseCaching();
// Configure the HTTP request pipeline.
app.UseRequestLogging();
app.UseExceptionHandler("/error"); // THIS IS GLOBAL EXCEPTION HANDLER

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
