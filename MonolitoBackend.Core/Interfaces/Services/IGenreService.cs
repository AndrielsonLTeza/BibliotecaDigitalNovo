using System.Collections.Generic;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.DTOs;


namespace MonolitoBackend.Core.Interfaces.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO> GetGenreByIdAsync(int id);
        Task<GenreDTO> GetGenreWithBooksAsync(int id);
        Task<GenreDTO> CreateGenreAsync(CreateGenreDTO genreDto);
        Task<bool> UpdateGenreAsync(int id, UpdateGenreDTO genreDto);
        Task<bool> DeleteGenreAsync(int id);
    }
}