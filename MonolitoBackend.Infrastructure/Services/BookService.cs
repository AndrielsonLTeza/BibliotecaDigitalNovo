using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Interfaces.Repositories;
using MonolitoBackend.Core.Interfaces.Services;
using MonolitoBackend.Core.DTOs; 
using AutoMapper;  

namespace MonolitoBackend.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;  

        // Construtor corrigido
        public BookService(IBookRepository bookRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;  
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId)
        {
            var genreExists = await _genreRepository.ExistsAsync(genreId);
            if (!genreExists)
            {
                throw new KeyNotFoundException($"Genre with ID {genreId} not found.");
            }
            return await _bookRepository.GetByGenreIdAsync(genreId);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentException("Book title cannot be empty.", nameof(book.Title));
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                throw new ArgumentException("Book author cannot be empty.", nameof(book.Author));
            }

            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new ArgumentException("Book ISBN cannot be empty.", nameof(book.ISBN));
            }

            var genreExists = await _genreRepository.ExistsAsync(book.GenreId);
            if (!genreExists)
            {
                throw new KeyNotFoundException($"Genre with ID {book.GenreId} not found.");
            }

            return await _bookRepository.AddAsync(book);
        }

        public async Task<bool> UpdateBookAsync(int id, UpdateBookDTO bookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return false;

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.ISBN = bookDto.ISBN;
            book.PublishedYear = bookDto.PublishedYear;
            book.GenreId = bookDto.GenreId;

            var genreExists = await _genreRepository.ExistsAsync(book.GenreId);
            if (!genreExists)
                throw new KeyNotFoundException($"Genre with ID {book.GenreId} not found.");

            await _bookRepository.UpdateAsync(book);
            return true;
        }


        public async Task<bool> DeleteBookAsync(int id)
        {
            var exists = await _bookRepository.ExistsAsync(id);
            if (!exists)
                return false;

            await _bookRepository.DeleteAsync(id);
            return true;
        }
        

        // Correção no método GetBooksByGenreAsync para retornar BookDTO
        public async Task<IEnumerable<BookDTO>> GetBooksByGenreAsync(int genreId)
        {
            var genreExists = await _genreRepository.ExistsAsync(genreId);
            if (!genreExists)
            {
                throw new KeyNotFoundException($"Genre with ID {genreId} not found.");
            }

            var books = await _bookRepository.GetByGenreIdAsync(genreId);

            // Mapeando os livros para BookDTO usando o AutoMapper
            var bookDTOs = _mapper.Map<IEnumerable<BookDTO>>(books);

            return bookDTOs;
        }
    }
}
