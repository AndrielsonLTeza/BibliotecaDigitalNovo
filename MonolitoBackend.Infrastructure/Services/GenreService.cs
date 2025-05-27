using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Interfaces.Repositories;
using MonolitoBackend.Core.Interfaces.Services;

namespace MonolitoBackend.Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreDTO>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            if (genres == null)
            {
                throw new KeyNotFoundException("Genres not found.");
            }

            // Converter para DTOs
            var genreDTOs = genres.Select(g => new GenreDTO
            {
                Id = g.Id, // Garantindo que o ID seja atribuído
                Name = g.Name,
                Description = g.Description,
            }).ToList(); // Garantindo que `genreDTOs` seja uma lista não nula

            return genreDTOs;
        }

        public async Task<GenreDTO> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found.");
            }

            // Converter para DTO
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description
            };
        }
    public async Task<GenreDTO> GetGenreWithBooksAsync(int id)
        {
            var genre = await _genreRepository.GetByIdWithBooksAsync(id);
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found.");
            }

            // Converter para DTO
            return new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name,
                Description = genre.Description,
            };
        }

        public async Task<GenreDTO> CreateGenreAsync(CreateGenreDTO genreDto)
        {
            if (genreDto == null)
            {
                throw new ArgumentNullException(nameof(genreDto));
            }

            var genre = new Genre
            {
                Name = genreDto.Name,
                Description = genreDto.Description,
                Books = new List<Book>() // Inicializando Books como uma lista vazia
            };

            var createdGenre = await _genreRepository.AddAsync(genre);

            // Agora garantindo que o Id está presente
            return new GenreDTO
            {
                Id = createdGenre.Id, // Certificando-se de que o ID seja atribuído corretamente
                Name = createdGenre.Name,
                Description = createdGenre.Description,
            };
        }


        public async Task<bool> UpdateGenreAsync(int id, UpdateGenreDTO genreDto)
        {
            if (genreDto == null)
            {
                throw new ArgumentNullException(nameof(genreDto));
            }

            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found.");
            }

            genre.Name = genreDto.Name;
            genre.Description = genreDto.Description;

            await _genreRepository.UpdateAsync(genre);
            return true;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                throw new KeyNotFoundException($"Genre with ID {id} not found.");
            }

            await _genreRepository.DeleteAsync(id);
            return true;
        }

    }
}

