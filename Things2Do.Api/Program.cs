using Serilog; 
using Things2Do.Api.Endpoints;
using Things2Do.Api.Services;

// var builder = WebApplication.CreateBuilder();

//TODO -- temp !
var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = @"..\..\frontend\wwwroot"
});

builder.Configuration.AddEnvironmentVariables();

// builder.WebHost.UseWebRoot(@"..\..\frontend\wwwroot");

builder.Services.AddHttpClient<HereService>();

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() //FIXME -- replace origin w/ domain name
              .WithMethods("POST")
              .AllowAnyHeader(); //May want 2 customize? but prob fine
    });
});

//Global Serilog logger for debugging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddSerilog();

var app = builder.Build();

// app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors();

app.MapSearchEndpoints();

app.Run();