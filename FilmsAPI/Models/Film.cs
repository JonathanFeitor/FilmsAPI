using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Models;

public class Film
{
    [Key]
    [Required]
    public int Id { get; internal set; }
    [Required(ErrorMessage = "The title of the film is mandatory!")]
    public string Title { get; set; }
    [Required(ErrorMessage = "The genre of the film is mandatory!")]
    [MaxLength(50, ErrorMessage = "The length of the film genre must not exceed 50 characters!")]
    public string Genre { get; set; }
    [Required(ErrorMessage = "The Duration of the film is mandatory!")]
    [Range(70, 600, ErrorMessage = "The length of the film has be between 70 and 600 minutes!")]
    public string Duration { get; set; }

   
}
