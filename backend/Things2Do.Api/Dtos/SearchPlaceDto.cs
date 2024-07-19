using System.ComponentModel.DataAnnotations;

namespace Things2Do.Api.Dtos;

//Dto that represents a request to find places around a specific locatoin
public record class SearchPlaceDto
(
    [Required] [Range(-90, 90)]
    decimal Lat,
    [Required] [Range(-180, 180)]
    decimal Lng
);