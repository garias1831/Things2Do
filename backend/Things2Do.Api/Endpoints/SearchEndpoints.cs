using Microsoft.OpenApi.Services;
using Serilog;
using Things2Do.Api.Data.Deserialization;
using Things2Do.Api.Dtos;
using Things2Do.Api.Dtos.Factories;
using Things2Do.Api.Dtos.OpeningHours;
using Things2Do.Api.Filtering;
using Things2Do.Api.Services;

namespace Things2Do.Api.Endpoints;

//Sets up endpoints for the searching function
public static class SearchEndpoints
{
    public static RouteGroupBuilder MapSearchEndpoints(this WebApplication app)
    {
        //WithParameterValidation() from MinimalApis.Extensions package
        var group = app.MapGroup("search").WithParameterValidation(); 
        
        //Location + filter information transmitted in body
        // POST /search
        group.MapPost("/", async (SearchPlaceDto request, HereService hereApi) => {
            
            //This part for loggin
            int off = request.Filters.Hours.TimeZoneOffset;
            // Log.Information($"Timezone offset: {off}");
            // Log.Information($"Distance: {request.Filters.Distance}"); 
            Log.Information($"Hours: {request.Filters.Hours.Start.AddMinutes(off)} - {request.Filters.Hours.End.AddMinutes(off)}"); //conv to local
            // Log.Information($"Types: {request.Filters.TypeFilters}");

            //May export this into seperate method but this works
            try 
            {
                //Send request to the here api + apply pre-filtering
                List<PlaceDeserialized> placesDeserialized = 
                    await hereApi.GetPlacesAsync(request);

                //Convert to a result DTO
                var places = placesDeserialized //Formatting goofy
                    .Where(pl => pl.ResultType == "place") //Filter out streets, towns, etc
                    .Select(pl => //map fn
                    {
                        //Pack in info DTOs
                        ContactResultDto? contacts = PlaceResultInfoFactory.CreateContactDto(pl);
                        OpeningHoursResultDto? openingHours = PlaceResultInfoFactory.CreateOpeningHoursDto(pl);

                        return new PlaceResultDto(
                            Title: pl.Title,
                            Lat: pl.Position.Lat,
                            Lng: pl.Position.Lng,
                            Address: pl.Address.Label,
                            Contacts: contacts,
                            OpeningHours: openingHours
                        );
                    })
                    .Where(dto => 
                    {
                        //Apply post-filtering (just opening hours for rn)
                        var filtering = new PostPlaceFiltering();

                        return filtering.IsOpenBetween(request, dto);
                        
                    });
                
                //FOR LOGGING -- excecute th query to see log statements
                //places.ToList();

                return Results.Ok(places);
                
                //FOR TESTING -- to see result of requests
                //return Results.Ok(placesDeserialized);

            }
            catch (HttpRequestException e)
            {
                //NOTE -- may want to inform the client somehow?
                Log.Error($"HTTP error: {e.Message}");
                return Results.StatusCode(500); 
            }

        });

        return group!;
    }
}