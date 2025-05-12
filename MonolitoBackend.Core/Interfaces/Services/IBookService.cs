using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.DTOs;

namespace MonolitoBackend.Core.Interfaces.Services
{
  public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book> GetBookByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByGenreIdAsync(int genreId);
    Task<Book> CreateBookAsync(Book book);
    Task<bool> UpdateBookAsync(int id, UpdateBookDTO bookDto);
    Task<bool> DeleteBookAsync(int id);
    Task<IEnumerable<BookDTO>> GetBooksByGenreAsync(int genreId);
}

}