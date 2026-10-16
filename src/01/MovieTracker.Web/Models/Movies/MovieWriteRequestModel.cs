using System.ComponentModel.DataAnnotations;

namespace MovieTracker.Web.Models.Movies;

public sealed class MovieWriteRequestModel
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Genre { get; set; } = "Drama";

    [Range(1888, int.MaxValue)]
    public int ReleaseYear { get; set; } = 2000;

    [Required]
    public string PosterUrl { get; set; } = string.Empty;
}
