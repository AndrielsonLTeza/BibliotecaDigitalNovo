using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetByGenreIdAsync(int genreId);
        Task<Book> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
