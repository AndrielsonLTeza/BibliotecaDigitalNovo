using System.ComponentModel.DataAnnotations;

namespace MonolitoBackend.Core.DTOs
{
    public class BookDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required int PublishedYear { get; set; }
        public required int GenreId { get; set; }
        public required string GenreName { get; set; } // Propriedade de navegação simplificada
    }
}