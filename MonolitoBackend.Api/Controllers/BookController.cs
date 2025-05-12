using Microsoft.AspNetCore.Mvc;
using MonolitoBackend.Core.Interfaces.Services;
using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AutoMapper;

namespace MonolitoBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtém todos os livros
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Obtém um livro pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        /// <summary>
        /// Obtém livros por gênero
        /// </summary>
        [HttpGet("by-genre/{genreId}")]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), 200)]
        public async Task<IActionResult> GetByGenre(int genreId)
        {
            var books = await _bookService.GetBooksByGenreAsync(genreId);
            return Ok(books);
        }

        /// <summary>
        /// Cria um novo livro
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BookDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateBookDTO createBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = _mapper.Map<Book>(createBookDto);
            var createdBook = await _bookService.CreateBookAsync(book);
            var bookDto = _mapper.Map<BookDTO>(createdBook);

            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);
        }

        /// <summary>
        /// Atualiza um livro existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDTO bookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var success = await _bookService.UpdateBookAsync(id, bookDto);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove um livro
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookService.DeleteBookAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
