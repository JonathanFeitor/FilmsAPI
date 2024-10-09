using System.ComponentModel.DataAnnotations;

namespace FilmsAPI.Data.DTOs;

public class ReadFilmDto
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Duration { get; set; }
    public DateTime AppointmentTime { get; set; } = DateTime.Now;


}
