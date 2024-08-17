using System.Net;
using Serilog; 
using Things2Do.Api.Endpoints;
using Things2Do.Api.Services;

var builder = WebApplication.CreateBuilder();
builder.Configuration.AddEnvironmentVariables();

//Config kestrel sever to accept https connections
builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    //FIXME (?) -- need to see if this works in production 
    int insecurePort;
    int securePort;
    if (builder.Environment.IsDevelopment())
    {
        insecurePort = 5000;
        securePort = 5001;
    }
    else
    {
        insecurePort = 80;
        securePort = 443;
    }

    //Overrides appsettings.json and launchsettings.json
    serverOptions.Listen(IPAddress.Loopback, insecurePort);
    serverOptions.Listen(IPAddress.Loopback, securePort, listenOptions =>
    {
        listenOptions.UseHttps(
            "things2doapp.com.pfx", 
            Environment.GetEnvironmentVariable("CERT_PASS")
        );
    });
});

builder.Services.AddHttpClient<HereService>();

builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin() //FIXME -- replace origin w/ domain name
              .AllowAnyMethod()
              .AllowAnyHeader(); //May want 2 customize? but prob fine
    });
});

//Global Serilog logger for debugging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddSerilog();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseDefaultFiles(); //Need this to display index.html on startup
app.UseStaticFiles();

app.UseCors();

app.MapSearchEndpoints();

app.Run();