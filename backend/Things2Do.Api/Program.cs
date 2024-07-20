using Things2Do.Api.Endpoints;
using Things2Do.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddHttpClient<HereService>();

var app = builder.Build();


app.UseStaticFiles();


app.MapSearchEndpoints();



app.Run();
