using System.ComponentModel.DataAnnotations;

namespace Things2Do.Api.Dtos;

//Information abt a place to be displayed to the client
//TODO -- not sure if data needs 2 be validated here, bc its a server response, remove l8er
public record class PlaceResultDto 
(
    [Required]
    string Title,

    [Required] [Range(-90, 90)]
    decimal Lat,

    [Required] [Range(-180, 180)]
    decimal Lng

    //Todo -- add contact info, images, reviews, etc
);