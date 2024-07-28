using Things2Do.Api.Data.Deserialization;
using Things2Do.Api.Dtos;
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
                List<PlaceDeserialized> places = 
                    await hereApi.GetPlacesAsync(searchQuery.Lat, searchQuery.Lng);

                return Results.Ok(places);

            }
            catch (HttpRequestException e)
            {
                //NOTE -- may want to inform the client somehow?
                Console.Error.WriteLine($"HTTP error: {e.Message}");
                return Results.BadRequest(); //FIXME Prob want to return something else
            }

        });

        return group!;
    }
}