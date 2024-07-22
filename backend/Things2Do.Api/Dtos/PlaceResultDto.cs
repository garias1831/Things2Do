using System.ComponentModel.DataAnnotations;

namespace Things2Do.Api.Dtos;

//Information abt a place to be displayed to the client
public record class PlaceResult 
(
    [Required] [Range(-90, 90)]
    decimal Lat,

    [Required] [Range(-180, 180)]
    decimal Lng

    //Todo -- add contact info, images, reviews, etc
);