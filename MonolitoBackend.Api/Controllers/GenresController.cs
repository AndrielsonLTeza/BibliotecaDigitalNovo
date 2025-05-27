using Microsoft.AspNetCore.Mvc;
using MonolitoBackend.Core.Interfaces.Services;
using MonolitoBackend.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MonolitoBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IBookService _bookService;

        public GenresController(IGenreService genreService, IBookService bookService)
        {
            _genreService = genreService;
            _bookService = bookService;
        }

        /// <summary>
        /// Obtém todos os gêneros
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenreDTO>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        /// <summary>
        /// Obtém um gênero pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenreDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound();

            return Ok(genre);
        }

        /// <summary>
        /// Obtém todos os livros de um gênero específico
        /// </summary>
        [HttpGet("{id}/books")]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetBooks(int id)
        {
            // Verificar se o gênero existe
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound();

            var books = await _bookService.GetBooksByGenreAsync(id);
            return Ok(books);
        }

        /// <summary>
        /// Cria um novo gênero
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(GenreDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateGenreDTO genreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdGenre = await _genreService.CreateGenreAsync(genreDto);
                return CreatedAtAction(nameof(GetById), new { id = createdGenre.Id }, createdGenre);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message} \n\n Inner: {ex.InnerException?.Message} \n\n StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Atualiza um gênero existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGenreDTO genreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _genreService.UpdateGenreAsync(id, genreDto);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message} \n\n Inner: {ex.InnerException?.Message} \n\n StackTrace: {ex.StackTrace}");
            }
        }
            /// <summary>
            /// Remove um gênero
            /// </summary>
            [HttpDelete("{id}")]
            [ProducesResponseType(204)]
            [ProducesResponseType(404)]
            [ProducesResponseType(400)]

            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    var success = await _genreService.DeleteGenreAsync(id);
                    if (!success)
                        return NotFound();

                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro: {ex.Message} \n\n Inner: {ex.InnerException?.Message} \n\n StackTrace: {ex.StackTrace}");
                }
            }
    }
}
