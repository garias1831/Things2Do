using Serilog;
using Things2Do.Api.Data.Deserialization;
using Things2Do.Api.Dtos;
using Things2Do.Api.Dtos.Factories;
using Things2Do.Api.Dtos.OpeningHours;
using Things2Do.Api.Services;

namespace Things2Do.Api.Endpoints;

//Sets up endpoints for the searching function
public static class SearchEndpoints
{
    // private static void search()
    // {

    // }

    public static RouteGroupBuilder MapSearchEndpoints(this WebApplication app)
    {
        //WithParameterValidation() from MinimalApis.Extensions package
        var group = app.MapGroup("search").WithParameterValidation(); 
        
        //Location + filter information transmitted in body
        // POST /search
        group.MapPost("/", async (SearchPlaceDto searchQuery, HereService hereApi) => {
           
            //May export this into seperate method but this works
            try //This stuff not done yet
            {
                List<PlaceDeserialized> placesDeserialized = 
                    await hereApi.GetPlacesAsync(searchQuery.Lat, searchQuery.Lng);

                //Convert to a result DTO
                var places = placesDeserialized //Formatting goofy
                    .Where(pl => pl.ResultType == "place") //Filter out streets, towns, etc
                    .Select(pl => //map fn
                    {
                        
                        //Pack data into contact dto
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
                    });
                
                //FOR LOGGING -- excecute th query to see log statements
                places.ToList();

                return Results.Ok(places);
                
                //FOR TESTING -- to see result of requests
                //return Results.Ok(placesDeserialized);

            }
            catch (HttpRequestException e)
            {
                //NOTE -- may want to inform the client somehow?
                Console.Error.WriteLine($"HTTP error: {e.Message}");
                return Results.StatusCode(500); //FIXME Prob want to return something else
            }

        });

        return group!;
    }
}