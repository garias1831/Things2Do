using Things2Do.Api.Data.Here;
using Things2Do.Api.Dtos.SearchFilters;

namespace Things2Do.Api.Filtering;

//Filter results BEFORE making an api call (via query-strings)
//Methods here should return strings
public class PrePlaceFiltering
{  
    //Returns a comma separated string of HERE place category codes 
    public string GetPlaceCategoryCodes(TypeFiltersDto typeDto)
    {
        List<string> codes = new List<string>();

        Type filterDtoType = typeof(TypeFiltersDto);
        var propVals= filterDtoType.GetProperties()
                                       .Select(p => (bool)p.GetValue(typeDto)!);

        //If all the type values are false, add a set of generic defaults
        if (!propVals.Aggregate(false, (bool left, bool right) => left || right)) //returns tru if everything is tru
        {
            //todo - OPTIMIZE THIS
            codes.AddRange([
                PlaceCategories.Restaurant,
                PlaceCategories.NightlifeEntertainment,
                PlaceCategories.TouristAttraction,
                PlaceCategories.Museum,
                PlaceCategories.ShoppingMall
            ]);
            return String.Join(',', codes);
        }

        //prospective software engineer
        if (typeDto.FoodDrink)
        {
            codes.AddRange([
                PlaceCategories.Restaurant,
                PlaceCategories.CoffeeTea,
                PlaceCategories.Winery,
                PlaceCategories.Brewery,
                PlaceCategories.Distillery
            ]);
        }
        if (typeDto.Entertainment)
        {
            codes.AddRange([
                PlaceCategories.NightlifeEntertainment,
                PlaceCategories.Cinema,
                PlaceCategories.TheatreMusicCulture,
                PlaceCategories.GamblingLotteryBetting
            ]);
        }
        if (typeDto.Museums)
        {
            codes.AddRange([
                PlaceCategories.Museum,
                PlaceCategories.Gallery
            ]);
        }
        if (typeDto.LandmarksMonuments)
        {
            codes.AddRange([
                PlaceCategories.TouristAttraction,
                PlaceCategories.HistoricalMonument,
                PlaceCategories.Castle,
                PlaceCategories.NamedIntersection,
                PlaceCategories.ReligiousPlace
            ]);
        }
        if (typeDto.Shopping)
        {
            codes.AddRange([
                PlaceCategories.ShoppingMall,
                PlaceCategories.FoodBeverageStore,
                PlaceCategories.OtherBookshop,
                PlaceCategories.Bookstore,
                PlaceCategories.SportingGoodsStore,
                PlaceCategories.FlowersAndJewelry,
                PlaceCategories.GiftAntiqueArt,
                PlaceCategories.RecordCDVideo,
                PlaceCategories.Market,
                PlaceCategories.Florist,
                PlaceCategories.Jeweler,
                PlaceCategories.ToyStore,
                PlaceCategories.InternetCafe
            ]);
        }
        if (typeDto.OutdoorRecreation)
        {
            codes.AddRange([
                PlaceCategories.OutdoorRecreation,
                PlaceCategories.SportsFacilityVenue
            ]);
        }
        if (typeDto.ThemeparksZoosAquariums)
        {
            //TODO NEED TO FIX THIS ONE -> more refined categories = better
            codes.AddRange([
                PlaceCategories.Leisure,
                PlaceCategories.AmusementPark,
                PlaceCategories.Zoo,
                PlaceCategories.WildAnimalPark,
                PlaceCategories.WildlifeRefuge,
                PlaceCategories.Aquarium,
                PlaceCategories.SkiResort,
                PlaceCategories.WaterPark
            ]);
        }
        
        return String.Join(',', codes);
    }
}