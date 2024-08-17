namespace Things2Do.Api.Data.Here;
//TODO -- move the deserialization stuff here mayb?

//Stores (a subset of) the category codes for places in the here api
public static class PlaceCategories
{
    //100 - Eat and Drink
    public static string Restaurant { get {return "100-1000-0000";} }

    public static string CoffeeTea { get {return "100-1100-0000";}}


    //200 - Going Out-Entertainment
    public static string NightlifeEntertainment { get {return "200-2000-0000";}}

    public static string Cinema { get {return "200-2100-0019";}}

    public static string TheatreMusicCulture { get {return "200-2200-0000";}}

    public static string GamblingLotteryBetting { get {return "200-2300-0000";}}


    //300 - Sights and Museums
    public static string TouristAttraction { get {return "300-3000-0023";}}
    
    public static string Gallery { get {return "300-3000-0024";}}

    public static string HistoricalMonument { get {return "300-3000-0025";}}

    public static string Castle { get {return "300-3000-0030";}}

    public static string NamedIntersection { get {return "300-3000-0232";}}

    public static string Winery { get {return "300-3000-0065";}}

    public static string Brewery { get {return "300-3000-0350";}}

    public static string Distillery { get {return "300-3000-0351";}}

    public static string Museum { get {return "300-3100-0000";}}

    public static string ReligiousPlace { get {return "300-3200-0000";}}


    //550 - Leisure and Outdoor

    public static string OutdoorRecreation { get {return "550-5510-0000";}}

    public static string Leisure { get {return "550-5520-0000";}}

    public static string AmusementPark { get {return "550-5520-0207";}}

    public static string Zoo { get {return "550-5520-0208";}}

    public static string WildAnimalPark { get {return "550-5520-0209";}}

    public static string WildlifeRefuge { get {return "550-5520-0210";}}

    public static string Aquarium { get {return "550-5520-0211";}}

    public static string SkiResort { get {return "550-5520-0212";}}

    public static string WaterPark { get {return "550-5520-0357";}}

    //600 - Shopping
    public static string ShoppingMall { get {return "600-6100-0062";}}

    public static string FoodBeverageStore { get {return "600-6300-0064";}}

    public static string OtherBookshop { get {return "600-6700-0000";}}

    public static string Bookstore { get {return "600-6700-0000";}}

    public static string SportingGoodsStore { get {return "600-6900-0095";}}

    public static string FlowersAndJewelry { get {return "600-6900-0101";}}

    public static string GiftAntiqueArt { get {return "600-6900-0103";}}

    public static string RecordCDVideo { get {return "600-6900-0105";}}

    public static string Market { get {return "600-6900-0247";}}

    public static string Florist { get {return "600-6900-0355";}}

    public static string Jeweler { get {return "600-6900-0356";}}

    public static string ToyStore { get {return "600-6900-0358";}}


    //700 - Businesses and Services

    public static string Farm { get {return "700-7200-0257";}}

    public static string InternetCafe { get {return "700-7200-0285";}}

    //800 - Facilities

    public static string SportsFacilityVenue { get {return "800-8600-0000";}}

}