namespace Things2Do.Api.Dtos;

//Information abt a place to be displayed to the client
public record class PlaceResultDto 
(
    string Title,

    decimal Lat,

    decimal Lng,

    string Address,

    ContactResultDto? Contacts

    //OpeningHoursResultDto? OpeningHours

    //TODO Need 2 add opening hours + image link 

    //Todo -- add contact info, images, reviews, etc
    
);