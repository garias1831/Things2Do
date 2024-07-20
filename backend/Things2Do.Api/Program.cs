using Things2Do.Api.Endpoints;
using Things2Do.Api.Serivces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<HereService>();


var app = builder.Build();


app.UseStaticFiles();


app.MapSearchEndpoints();



app.Run();
