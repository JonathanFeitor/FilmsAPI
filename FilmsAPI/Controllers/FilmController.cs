using AutoMapper;
using FilmsAPI.Data;
using FilmsAPI.Data.DTOs;
using FilmsAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace FilmsAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class FilmController : ControllerBase
{
    private FilmContext _context;
    private IMapper _mapper;

    public FilmController(FilmContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Summary
    // Adds a film to the database
    //Object with the fields needed to create a film
    //Response code="201" If insertion is successful
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddFilm([FromBody] CreateFilmDto filmDto)
    {
        Film film = _mapper.Map<Film>(filmDto);
        _context.Films.Add(film);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecoveryFilmById), new { id = film.Id }, film);
    }

    //Return using paging resources
    [HttpGet]
    public IEnumerable<ReadFilmDto> ReadFilms([FromQuery] int skip = 0, [FromQuery] int take = 30)
    {
        return _mapper.Map<List<ReadFilmDto>>(_context.Films.Skip(skip).Take(take));
    }
     
    //Return using an Id to retriave the film
    [HttpGet("{id}")]
    public IActionResult RecoveryFilmById(int id)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);
        if (film == null) return NotFound();
        var filmDto = _mapper.Map<ReadFilmDto>(film);
        return Ok(filmDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFilm(int id, [FromBody] UpdateFilmDto filmDto)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);

        if(film == null) return NotFound();
        _mapper.Map(filmDto, film);
        _context.SaveChanges();
        return NoContent();

    }

    [HttpPatch("{id}")]
    public IActionResult UpdatePartialFilm(int id, JsonPatchDocument<UpdateFilmDto> patch)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);

        if(film == null) return NotFound(); 

        var filmToUpdate = _mapper.Map<UpdateFilmDto>(film);
        patch.ApplyTo(filmToUpdate, ModelState);

        if (!TryValidateModel(filmToUpdate))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmToUpdate, film);
        _context.SaveChanges();
        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFilm(int id)
    {
        var film = _context.Films.FirstOrDefault(film => film.Id == id);
        if(film == null) return NotFound();

        _context.Remove(film);
        _context.SaveChanges();
        return NoContent();
    }

}
