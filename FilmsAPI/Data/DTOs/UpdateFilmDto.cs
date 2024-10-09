using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs;

public class UpdateFilmDto
{
  
    [Required(ErrorMessage = "The title of the film is mandatory!")]
    public string Title { get; set; }
    [Required(ErrorMessage = "The genre of the film is mandatory!")]
    [StringLength(50, ErrorMessage = "The length of the film genre must not exceed 50 characters!")]
    public string Genre { get; set; }
    [Required(ErrorMessage = "The Duration of the film is mandatory!")]
    [Range(70, 600, ErrorMessage = "The length of the film has be between 70 and 600 minutes!")]
    public string Duration { get; set; }
}
