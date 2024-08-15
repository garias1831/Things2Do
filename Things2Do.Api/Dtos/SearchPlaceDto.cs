using System.ComponentModel.DataAnnotations;
using Things2Do.Api.Dtos.SearchFilters;

namespace Things2Do.Api.Dtos;

//Dto that represents a request to find places around a specific location
public record class SearchPlaceDto
(
    [Required] [Range(-90, 90)]
    decimal Lat,
    
    [Required] [Range(-180, 180)]
    decimal Lng,

    [Required]
    SearchFiltersDto Filters
);