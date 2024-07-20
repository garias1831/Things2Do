using Things2Do.Api.Dtos;

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
        group.MapPut("/", (SearchPlaceDto searchQuery) => {

        });

        return group!;
    }
}