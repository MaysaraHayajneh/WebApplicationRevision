using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using WebApplicationRevision;
using WebApplicationRevision.AuthunticationHndlerFolder;
using WebApplicationRevision.Constants.enums;
using WebApplicationRevision.Contratct;
using WebApplicationRevision.CustomModelBinding;
using WebApplicationRevision.Filters.ActionFilters;
using WebApplicationRevision.Filters.AuthorizationFilter;
using WebApplicationRevision.Filters.ExceptionFilter;
using WebApplicationRevision.Filters.ResultFiltet;
using WebApplicationRevision.Middlewares;
using WebApplicationRevision.OptionPatternsClasses;
using WebApplicationRevision.OptionPatternsClasses.Validators;
using WebApplicationRevision.Services;
using WebApplicationRevision.Services.Contract;

var builder = WebApplication.CreateBuilder(args);

#region Logging
// => ADD LOGGING PROVIDER (LOGGING SINK)
builder.Services.AddLogging((ILoggingBuilder logBuilder) =>
{
	logBuilder.AddDebug();
});
#endregion

#region Filters
// Add services to the container.
builder.Services.AddControllers((MvcOptions opt) =>
{
	//opt.Filters.Add<LogActivityFilter>(); // THIS IS GLOBAL FILTER
	opt.Filters.Add<LogActivityFilterAsync>(); // THIS IS GLOBAL FILTER
											   //opt.Filters.Add<CacheFilterFilter>(); // THIS IS GLOBAL FILTER
											   //opt.Filters.Add<CacheFilterFilter2>(); // THIS IS GLOBAL FILTER
											   //opt.ValueProviderFactories.Add(new CustomValueProviderFactory()); // THIS IS GLOBAL VALUE PROVIDER FACTORY

	opt.Filters.Add<GlobalExceptionFilter>(); // THIS IS GLOBAL FILTER
	opt.Filters.Add<ResponseWrappingResultFilter>(); // THIS IS GLOBAL FILTER
													 //opt.Filters.Add<CustomAuthorize>(); // THIS IS GLOBAL FILTER


	opt.ValueProviderFactories.Insert(0, new CookieValueProviderFactory());
});
#endregion

#region services

builder.Services.AddDbContext<AppDbContext>((DbContextOptionsBuilder opt) =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), (SqlServerDbContextOptionsBuilder sqlop) =>
	{
		sqlop.EnableRetryOnFailure();
	});
});
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

builder.Services.Configure<ApiBehaviorOptions>((ApiBehaviorOptions opt) =>
{
	opt.SuppressModelStateInvalidFilter = true;
});

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


// => ADD KEYED SERVICES 

builder.Services.AddKeyedScoped<INotificationService, EmailNotificationService>(NotificationEnum.EMAIL);
builder.Services.AddKeyedScoped<INotificationService, SmsNotificationService>(NotificationEnum.SMS);



#endregion

#region Authuntecation

// ==> BASIC AUTHENTICATION
builder.Services.AddAuthentication()
	.AddScheme< AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

#endregion


#region Configuration

builder.Services.AddOptions<WeatherOptions>()
	.BindConfiguration(WeatherOptions.SectionName, (BinderOptions opt) =>
	{
		opt.ErrorOnUnknownConfiguration = false; // enable an compile error if a key missing in configuration while binding
	}).ValidateDataAnnotations() // enabling data annotaion validation
	   .ValidateOnStart();//for the options class and if the validation failed it will throw an exception on application start up and prevent the application from running

builder.Services.AddSingleton<IValidateOptions<WeatherOptions>, WeatherOptionsValidator>();

// ADD NEW CONFIGURATION FILE

builder.Configuration.AddJsonFile("CustomConfig.json", false, true); // giveing the name without full path means the file in same app folder


// => NAMED OPTIONS

builder.Services.AddOptions<NotificationOptions>(NotificationOptions.Email)
	.BindConfiguration("NotificationOptions:Email", (BinderOptions opt) =>
	{
		opt.ErrorOnUnknownConfiguration = false;

	}).ValidateDataAnnotations()
	.ValidateOnStart();

builder.Services.AddOptions<NotificationOptions>(NotificationOptions.SMS)
	.BindConfiguration("NotificationOptions:Sms", (BinderOptions opt) =>
	{
		opt.ErrorOnUnknownConfiguration = false;

	}).ValidateDataAnnotations()
	.ValidateOnStart();


// POST CONFIGURE
// TO CHANGE TEH OPTIONS AFTER THEY GET CONFIGURE AND BINDED 
// IT HELPS IF I WILL GIVE DEFAULT VALUES BASED ON OTEHR CONFIG VALUES
//READ CONFIG VALUES FROM OTHER COFIGURED Values
builder.Services.PostConfigure<NotificationOptions>("Sms", (opt) =>
{
	opt.Sender ??= "dasdas";
});

// POST CONFIGURE
// TO CHANGE TEH OPTIONS AFTER THEY GET CONFIGURE AND BINDED 
// IT HELPS IF I WILL GIVE DEFAULT VALUES BASED ON OTEHR CONFIG VALUES
//READ CONFIG VALUES FROM OTHER COFIGURED Values

builder.Services.PostConfigure<WeatherOptions>((WeatherOptions opt) =>
{
	opt.Summury = opt.Teampreature switch
	{
		<= 10 => "Cold",
		<= 25 => "Warm",
		_ => "Hot"
	};
});


/// ACCESS CONFIGURATION FROM ICONFIGURATION BEFORE IOPTIONS IS AVALILABLE BEFORE DI BUILD
/// 
builder.Configuration.GetSection(WeatherOptions.SectionName)
	.Get<WeatherOptions>();
#endregion


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build(); // on buildethe DI perform service validation 
app.UseResponseCaching();
// Configure the HTTP request pipeline.
app.UseRequestLogging();
app.UseExceptionHandler("/error"); // THIS IS GLOBAL EXCEPTION HANDLER

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
