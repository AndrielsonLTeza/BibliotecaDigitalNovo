using System.ComponentModel.DataAnnotations;
using MonolitoBackend.Core.Entities; // ou o namespace onde está a classe Genre

namespace MonolitoBackend.Core.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; } = string.Empty;
    }
}
