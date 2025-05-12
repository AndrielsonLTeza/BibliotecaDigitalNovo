using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(int id);
        Task<Genre?> GetByIdWithBooksAsync(int id);
        Task<Genre> AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}