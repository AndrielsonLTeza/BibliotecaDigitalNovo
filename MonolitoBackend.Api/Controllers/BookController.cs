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
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                var bookDtos = _mapper.Map<IEnumerable<BookDTO>>(books);
                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém um livro pelo ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                    return NotFound($"Livro com ID {id} não encontrado.");

                var bookDto = _mapper.Map<BookDTO>(book);
                return Ok(bookDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém livros por gênero
        /// </summary>
        [HttpGet("by-genre/{genreId}")]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), 200)]
        public async Task<IActionResult> GetByGenre(int genreId)
        {
            try
            {
                var books = await _bookService.GetBooksByGenreAsync(genreId);
                var bookDtos = _mapper.Map<IEnumerable<BookDTO>>(books);
                return Ok(bookDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria um novo livro
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BookDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateBookDTO createBookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var book = _mapper.Map<Book>(createBookDto);
                var createdBook = await _bookService.CreateBookAsync(book);
                var bookDto = _mapper.Map<BookDTO>(createdBook);

                return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um livro existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDTO updateBookDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingBook = await _bookService.GetBookByIdAsync(id);
                if (existingBook == null)
                    return NotFound($"Livro com ID {id} não encontrado.");

                await _bookService.UpdateBookAsync(id, updateBookDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
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
            try
            {
                var success = await _bookService.DeleteBookAsync(id);
                if (!success)
                    return NotFound($"Livro com ID {id} não encontrado.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}