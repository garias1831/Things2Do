using System.ComponentModel.DataAnnotations;

namespace Things2Do.Api.Dtos.SearchFilters;

//Controls what types of places should be shown in the response
public record class TypeFiltersDto
(
    [Required]
    bool FoodDrink,
    [Required]
    bool Entertainment,
    [Required]
    bool Museums,
    [Required]
    bool LandmarksMonuments,
    [Required]
    bool Shopping,
    [Required]
    bool OutdoorRecreation,
    [Required]
    bool ThemeparksZoosAquariums
);